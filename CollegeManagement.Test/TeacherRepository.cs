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
    public class TeacherRepository
    {
        protected IUnitOfWork _unitOfWork;
        public TeacherRepository()
        {
            this._unitOfWork = new UnitOfWork();
        }
        [TestMethod]
        public void Add()
        {
            Teacher teacher = new Teacher
            {
                SubjectID = 1,
                Name = "teacher1",
                BirthDate = DateTime.Now.AddYears(-18),
                Salary = 4000
            };
            _unitOfWork.TeacherRepository.Add(teacher);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Edit()
        {
            Teacher teacher = new Teacher
            {
                SubjectID = 1,
                Name = "teacher1111",
                BirthDate = DateTime.Now.AddYears(-18),
                Salary = 4500
            };
            _unitOfWork.TeacherRepository.Edit(teacher);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Delete()
        {
            _unitOfWork.TeacherRepository.Delete(1);
            _unitOfWork.Save();
        }
    }
}
