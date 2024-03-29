﻿using ASMSBusinessLayer.ContractsBLL;
using ASMSDataAccessLayer.ContractsDAL;
using ASMSEntityLayer.Models;
using ASMSEntityLayer.ResultModels;
using ASMSEntityLayer.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSBusinessLayer.ImplementationsBLL
{
    public class StudentBusinessEngine : IStudentBusinessEngine
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        public StudentBusinessEngine(IUnitOfWork unitofWork,
            IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public IResult Add(StudentVM student)
        {
            try
            {
                Student newStudent = _mapper.Map<StudentVM, Student>(student);
                var insertResult = _unitofWork.StudentRepo.Add(newStudent);
                return insertResult ?
                    new SuccessResult("Öğrenci eklendi") :
                    new ErrorResult("Öğrenci eklemede bir hata oluştu! Tekrar deneyiniz");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
