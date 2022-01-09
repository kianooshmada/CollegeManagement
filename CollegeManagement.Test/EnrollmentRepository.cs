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
    public class EnrollmentRepository
    {
        protected IUnitOfWork _unitOfWork;
        public EnrollmentRepository()
        {
            this._unitOfWork = new UnitOfWork();
        }
        [TestMethod]
        public void Add()
        {
            Enrollment enrollment = new Enrollment
            {
                StudentID = 1,
                SubjectID = 1,
                RegistrationNumber = 1001,
                RegistrationDate = DateTime.Now,
                Grade = 17.05
            };
            _unitOfWork.EnrollmentRepository.Add(enrollment);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Edit()
        {
            Enrollment enrollment = new Enrollment
            {
                EnrollmentID = 1,
                StudentID = 1,
                SubjectID = 1,
                RegistrationNumber = 8001,
                RegistrationDate = DateTime.Now,
                Grade = 18.25
            };
            _unitOfWork.EnrollmentRepository.Edit(enrollment);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Delete()
        {
            _unitOfWork.EnrollmentRepository.Delete(3);
            _unitOfWork.Save();
        }
    }
}
