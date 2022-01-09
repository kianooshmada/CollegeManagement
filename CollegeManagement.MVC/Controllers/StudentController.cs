using CollegeManagement.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeManagement.MVC.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            vmCreateStudent model = new vmCreateStudent();
            model.PageTitle = "Create Student";

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(vmCreateStudent model)
        {
            if (ModelState.IsValid)
            {
                if (model.SystemMessage == null)
                {
                    _unitOfWork.StudentRepository.Add(model.Student);
                    _unitOfWork.Save();

                    model.PageTitle = "Create Student";
                    return RedirectToAction("List");
                }
            }

            if (string.IsNullOrEmpty(model.Student.Name))
            {
                model.SystemMessage = new SystemMessage
                {
                    MessageText = "please enter the name",
                    success = false
                };
            }
            model.PageTitle = "Create Student";
            return View(model);
        }
        public ActionResult List(int page = 1, bool showError = false)
        {
            var studentPagedList = _unitOfWork.StudentRepository.GetStudentPagedList(page, 30);

            vmStudentPagedList model = new vmStudentPagedList
            {
                StudentPagedList = studentPagedList,
                PageTitle = "Student List",
            };

            if (showError)
            {
                model.SystemMessage = new SystemMessage
                {
                    MessageText = "you can not delete the student becuase it is used in other entities",
                    success = false
                };
            }

            model.Pager = new Pager
            {
                CurrentPage = studentPagedList.CurrentPage,
                TotalPage = studentPagedList.TotalPages,
                TotalCount = studentPagedList.TotalCount,
                ActionUrl = "List"
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult List(int? itemForDelete, int page = 1)
        {
            if (itemForDelete.HasValue)
            {
                if (!_unitOfWork.StudentRepository.HasRelationship((int)itemForDelete))
                {
                    _unitOfWork.StudentRepository.Delete((int)itemForDelete);
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

            var studentDetails = _unitOfWork.StudentRepository.GetStudentDetails(selectedID);
            if (studentDetails == null)
                return RedirectToAction("List");

            vmStudentDetails model = new vmStudentDetails()
            {
                Student = studentDetails,
                PageTitle = "Student Detail"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(vmStudentDetails model, string id)
        {
            int selectedID;
            if (!int.TryParse(id, out selectedID))
                return RedirectToAction("List");

            var studentDetails = _unitOfWork.StudentRepository.GetStudentDetails(selectedID);
            if (studentDetails == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                studentDetails.Name = model.Student.Name;
                studentDetails.BirthDate = model.Student.BirthDate;
                _unitOfWork.StudentRepository.Edit(studentDetails);
                _unitOfWork.Save();

                model.SystemMessage = new SystemMessage
                {
                    MessageText = "Information updated successfully",
                    success = true
                };

            }
            else
            {
                if (string.IsNullOrEmpty(model.Student.Name))
                {
                    model.SystemMessage = new SystemMessage
                    {
                        MessageText = "please enter the name",
                        success = false
                    };
                }
            }

            var studentDetail = _unitOfWork.StudentRepository.GetStudentDetails(selectedID);
            studentDetail.Name = model.Student.Name;
            studentDetail.BirthDate = model.Student.BirthDate;

            model.Student = studentDetail;
            model.PageTitle = "Student Detail";
            return View(model);
        }

        public ActionResult StudentListReport(int page = 1)
        {
            var studentList = _unitOfWork.StudentRepository.GetStudentReport(page, 30);

            vmStudentListReport model = new vmStudentListReport
            {
                StudentList = studentList,
                PageTitle = "Student List",
            };
            
            model.Pager = new Pager
            {
                CurrentPage = studentList.CurrentPage,
                TotalPage = studentList.TotalPages,
                TotalCount = studentList.TotalCount,
                ActionUrl = "StudentListReport"
            };

            return View(model);
        }
    }
}