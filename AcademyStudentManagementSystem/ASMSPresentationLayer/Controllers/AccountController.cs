﻿using ASMSBusinessLayer.EmailService;
using ASMSEntityLayer.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASMSBusinessLayer.ContractsBLL;
using ASMSPresentationLayer.Models;
using ASMSEntityLayer.Enums;
using ASMSEntityLayer.ViewModels;
using ASMSEntityLayer.ResultModels;
using ASMSBusinessLayer.ViewModels;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;

namespace ASMSPresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IStudentBusinessEngine _studentBusinessEngine;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IEmailSender emailSender, IStudentBusinessEngine studentBusinessEngine)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _studentBusinessEngine = studentBusinessEngine;
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //return View(model);
                    TempData["RegisterFailedMessage"] = "Veri girişlerini istenildiği gibi yapmadınız. Tekrar deneyiniz";
                    return RedirectToAction("Index", "Home");
                }

                //Aynı emailden tekrar kayıt olunmasın
                var checkUserForEmail = await _userManager.FindByEmailAsync(model.Email);
                if (checkUserForEmail != null)
                {
                    //ModelState.AddModelError("", "Bu email ile zaten sisteme kayıt yapılmıştır!");
                    //return View(model);
                    TempData["RegisterFailedMessage"] = "Bu mail adresi sistemde kayıtlıdır. Farklı bir mail ile kayıt olunuz.";
                    return RedirectToAction("Index", "Home");
                }
                //user'ı oluşturalım
                AppUser newUser = new AppUser()
                {
                    TCNumber=model.TCNumber,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    BirthDate = model.BirthDate.HasValue ?
                    model.BirthDate.Value : null,
                    Gender = model.Gender,
                    EmailConfirmed = true,
                    UserName = model.Email
                };

                var result = await _userManager
                    .CreateAsync(newUser, model.Password);
                if (result.Succeeded) //eklendi
                {
                    //rol ataması
                    var roleResult = await _userManager
                        .AddToRoleAsync(newUser, ASMSRoles.Student.ToString());
                    if (roleResult.Succeeded == false)
                    {
                        //Admine gizliden bir email gönder eklesin rolü
                    }
                    //Student eklensin
                    StudentVM newStudent = new StudentVM()
                    {
                        UserId = newUser.Id,
                        TCNumber = model.TCNumber
                    };
                    IResult resultStudent = _studentBusinessEngine.Add(newStudent);
                    if (resultStudent.IsSuccess == false)
                    {
                        //Admine gizliden bir email gönder eklesin öğrenciyi
                    }
                    //email gönderilsin
                    var emailToStudent = new EmailMessage()
                    {
                        Subject = "ASMS Sistemine HOŞ GELDİNİZ! " +
                        newUser.Name + " " + newUser.Surname,
                        Body = "Merhaba, Sisteme kaydınız gerçekleşmiştir...",
                        Contacts = new string[] { model.Email }
                    };
                    await _emailSender.SendMessage(emailToStudent);
                    TempData["RegisterSuccessMessage"] = "Sisteme kaydınız başarıyla gerçekleşmiştir.";
                    return RedirectToAction("Index", "Account", new { email = model.Email });

                }
                else
                {
                    TempData["RegisterFailedMessage"] = "Beklenmedik bir sorun oldu. Üye kaydı başarısız tekrar deneyiniz!";
                    return RedirectToAction("Index", "Home");
                    //ModelState.AddModelError("", "Beklenmedik bir sorun oldu. Üye kaydı başarısız tekrar deneyiniz!");
                    //return View(model);
                }
                
            }
            catch (Exception)
            {

                //loglanacak

                return RedirectToAction("Error", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await _userManager.FindByNameAsync(model.Email);
                // var user = _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Epostanız ya da şifreniz hatalıdır! Tekrar deneyiniz!");
                    return View();
                }
                //TODO: son parametre bool lockoutOnFailure ile ilgili
                //örnek yapalım
                var result = await _signInManager.PasswordSignInAsync
                    (user, model.Password, model.RememberMe, false);

                //TODO: son parametre bool lockoutOnFailure ile ilgili
                //if (result.IsLockedOut)
                //{
                //    DateTimeOffset d = user.LockoutEnd.Value;
                //}
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Epostanız ya da şifreniz hatalıdır! Tekrar deneyiniz!");
                    return View();
                }
                //artık hoşgeldi
                if (_userManager.IsInRoleAsync(user, ASMSRoles.Student.ToString()).Result)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (_userManager.IsInRoleAsync(user, ASMSRoles.Coordinator.ToString()).Result)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                if (_userManager.IsInRoleAsync(user, ASMSRoles.StudentAdministration.ToString()).Result)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu! Tekrar deneyiniz");
                //ex loglansın
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user==null)
                {
                    ViewBag.ResetPasswordSuccessMessage = "Şifre yenileme talebiniz alındı. Epostanızı kontrol ediniz.";
                    return View();
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var codeEncode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callBackUrl = Url.Action("ConfirmResetPassword", "Account", new
                {
                    userId = user.Id,
                    code = codeEncode
                }, protocol: Request.Scheme);
                var emailMessage = new EmailMessage()
                {
                    Contacts = new string[] { user.Email },
                    Subject="ASMS - Yeni Şifre Talebi",
                    Body=$"Merhaba {user.Name} {user.Surname},"+
                    $"</br> Yeni parola belirlemek için"+
                    $"<a href='{HtmlEncoder.Default.Encode(callBackUrl)}'> buraya </a> tıklayınız..."
                };
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ResetPasswordFailMessage = "Beklenmedik bir hata oluştu.Tekrar deneyiniz.";
                return View();
                //ex loglansın
            }
        }

        [HttpGet]
        public IActionResult ConfirmResetPassword(string userId,string code)//Birden fazla değer vrsa model daha iyi olur.
        {
            if (string.IsNullOrEmpty(userId)||string.IsNullOrEmpty(code))
            {
                ViewBag.ConfirmResetPasswordFailureMessage = "Beklenmedik hata";
                return View();
            }
            ResetPasswordViewModel model = new ResetPasswordViewModel()
            {
                UserId = userId,
                Code = code
            };
            return View(model);
        }

        [HttpPost]
        public async  Task<IActionResult> ConfirmResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user==null)
                {
                    ModelState.AddModelError("", "Kullanıcı Bulunamadı.");
                    //log mesajı yerleştir.
                    //throw new Exception();
                }
                var tokenDecoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));
                var result = await _userManager.ResetPasswordAsync(user, tokenDecoded, model.NewPassword);
                if (result.Succeeded)
                {
                    TempData["ConfirmResetPasswordSuccess"] = "Şifreniz başarıyla güncelleştirildi.";
                    return RedirectToAction("Login", "Account", new { email = user.Email });
                }
                else
                {
                    ModelState.AddModelError("", "Şifrenizin değiştirilme işleminde beklenmedik bir hata oluştu.Tekrar deneyiniz.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //ex loglanacak
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu. Tekrar deneyiniz.");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
