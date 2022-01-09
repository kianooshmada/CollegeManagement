using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollegeManagement.Core.Models;
using CollegeManagement.MVC.Models;
namespace CollegeManagement.MVC.Controllers
{
    public class CourseController : BaseController
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            vmCreateCourse model = new vmCreateCourse();
            model.PageTitle = "Create Course";

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(vmCreateCourse model)
        {
            if (ModelState.IsValid)
            {
                if (model.SystemMessage == null)
                {                    
                    _unitOfWork.CourseRepository.Add(model.Course);
                    _unitOfWork.Save();

                    model.PageTitle = "Create Course";
                    return RedirectToAction("List");
                }
            }

            if (string.IsNullOrEmpty(model.Course.Name))
            {
                model.SystemMessage = new SystemMessage
                {
                    MessageText = "please enter the name",
                    success = false
                };
            }
            model.PageTitle = "Create Course";
            return View(model);
        }
        public ActionResult List(int page = 1, bool showError = false)
        {
            var coursePagedList = _unitOfWork.CourseRepository.GetCoursePagedList(page, 30);

            vmCoursePagedList model = new vmCoursePagedList
            {
                CoursePagedList = coursePagedList,
                PageTitle = "Course List",
            };

            if (showError)
            {
                model.SystemMessage = new SystemMessage
                {
                    MessageText = "you can not delete the course becuase it is used in other entities",
                    success = false
                };
            }

            model.Pager = new Pager
            {
                CurrentPage = coursePagedList.CurrentPage,
                TotalPage = coursePagedList.TotalPages,
                TotalCount = coursePagedList.TotalCount,
                ActionUrl = "List"
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult List(int? itemForDelete, int page = 1)
        {
            if (itemForDelete.HasValue)
            {
                if (!_unitOfWork.CourseRepository.HasRelationship((int)itemForDelete))
                {
                    _unitOfWork.CourseRepository.Delete((int)itemForDelete);
                    _unitOfWork.Save();
                }
                else
                {
                    return List(page, true);
                }
            }
            return List(page);
        }
        public ActionResult Detail(string id)
        {
            int selectedID;
            if (!int.TryParse(id, out selectedID))
                return RedirectToAction("List");

            var courseDetails = _unitOfWork.CourseRepository.GetCourseDetails(selectedID);
            if (courseDetails == null)
                return RedirectToAction("List");

            vmCourseDetails model = new vmCourseDetails()
            {
                Course = courseDetails,
                PageTitle = "Course Detail"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(vmCourseDetails model, string id)
        {
            int selectedID;
            if (!int.TryParse(id, out selectedID))
                return RedirectToAction("List");

            var courseDetails = _unitOfWork.CourseRepository.GetCourseDetails(selectedID);
            if (courseDetails == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                courseDetails.Name = model.Course.Name;
                _unitOfWork.CourseRepository.Edit(courseDetails);
                _unitOfWork.Save();

                model.SystemMessage = new SystemMessage
                {
                    MessageText = "Information updated successfully",
                    success = true
                };

            }
            else
            {
                if (string.IsNullOrEmpty(model.Course.Name))
                {
                    model.SystemMessage = new SystemMessage
                    {
                        MessageText = "please enter the name",
                        success = false
                    };
                }                
            }

            var courseDetail = _unitOfWork.CourseRepository.GetCourseDetails(selectedID);
            courseDetail.Name = model.Course.Name;
            model.Course = courseDetail;
            model.PageTitle = "Course Detail";
            return View(model);
        }
        public ActionResult CourseListReport(int page = 1)
        {
            var courseList = _unitOfWork.CourseRepository.GetCourseReport(page, 30);

            vmCourseListReport model = new vmCourseListReport
            {
                CourseList = courseList,
                PageTitle = "Course List",
            };
            
            model.Pager = new Pager
            {
                CurrentPage = courseList.CurrentPage,
                TotalPage = courseList.TotalPages,
                TotalCount = courseList.TotalCount,
                ActionUrl = "CourseListReport"
            };

            return View(model);
        }
    }
}