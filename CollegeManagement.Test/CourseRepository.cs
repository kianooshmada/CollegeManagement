using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
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
    public class CourseRepository
    {
        protected IUnitOfWork _unitOfWork;
        public CourseRepository()
        {
            this._unitOfWork = new UnitOfWork();
        }
        [TestMethod]
        public void Add()
        {
            Course course = new Course
            {
                Name="First3"
            };
            _unitOfWork.CourseRepository.Add(course);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Edit()
        {
            Course course = new Course
            {
                CourseID = 1,
                Name = "First3"
            };
            _unitOfWork.CourseRepository.Edit(course);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void Delete()
        {
            _unitOfWork.CourseRepository.Delete(3);
            _unitOfWork.Save();
        }
        [TestMethod]
        public void GetCourseReport()
        {
            var courseReport = _unitOfWork.CourseRepository.GetCourseReport(1,30);            
        }
        
    }
}
