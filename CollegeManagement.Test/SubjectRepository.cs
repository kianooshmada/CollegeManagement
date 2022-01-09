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
    public class SubjectRepository
    {
        protected IUnitOfWork _unitOfWork;
        public SubjectRepository()
        {
            this._unitOfWork = new UnitOfWork();
        }
        [TestMethod]
        public void Add()
        {
            Subject subject = new Subject
            {
                CourseID = 1,
                Name = "First3"
            };
            _unitOfWork.SubjectRepository.Add(subject);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Edit()
        {
            Subject subject = new Subject
            {
                SubjectID = 1,
                CourseID = 1,
                Name = "Sub"
            };
            _unitOfWork.SubjectRepository.Edit(subject);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Delete()
        {
            _unitOfWork.SubjectRepository.Delete(3);
            _unitOfWork.Save();
        }

        [TestMethod]
        public void SubjectReport()
        {
            var subjectReport = _unitOfWork.SubjectRepository.GetSubjectReport(1,30);
        }
    }
}
