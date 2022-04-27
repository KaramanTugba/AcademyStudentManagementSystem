using ASMSEntityLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMSEntityLayer.Models;

namespace ASMSEntityLayer.IdentityModels
{
    public class AppUser:IdentityUser,IBase
    {
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage ="İsim gereklidir.")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="İsminiz 2 ile 50 karakter arasında olmalıdır.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim gereklidir.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyisminiz 2 ile 50 karakter arasında olmalıdır.")]
        public string Surname { get; set; }
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C. Kimlik numarası 11 karakter olmalıdır.")]
        public string TCNumber { get; set; }
        public string Picture { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Cinsiyet seçimi gereklidir.")]
        public Genders Gender { get; set; }
        public bool IsDeleted { get; set; } = false;

        //ilişkiler
        public virtual ICollection<UsersAddress> UsersAddresses { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
