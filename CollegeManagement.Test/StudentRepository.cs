using CollegeManagement.Core.Models;
using CollegeManagement.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Test
{
    [TestClass]
    public class StudentRepository
    {
        protected IUnitOfWork _unitOfWork;
        public StudentRepository()
        {
            this._unitOfWork = new UnitOfWork();
        }
        [TestMethod]
        public void Add()
        {
            Student student = new Student
            {
                Name = "Student1",
                BirthDate = DateTime.Now.AddYears(-8)                
            };
            _unitOfWork.StudentRepository.Add(student);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Edit()
        {
            Student student = new Student
            {
                StudentID = 1,
                Name = "Student1111",
                BirthDate = DateTime.Now.AddYears(-7)
            };
            _unitOfWork.StudentRepository.Edit(student);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Delete()
        {
            _unitOfWork.StudentRepository.Delete(3);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void GetStudentReport()
        {
           var studentReport = _unitOfWork.StudentRepository.GetStudentReport(1,30);
        }
    }
}
