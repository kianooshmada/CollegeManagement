using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using CollegeManagement.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeManagement.MVC.Controllers
{
    public class TeacherController : BaseController
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var subjectSelectListItems = _unitOfWork.SubjectRepository.GetSubjectSelectListItem();
            TempData["subjectSelectListItems"] = subjectSelectListItems;

            vmCreateTeacher model = new vmCreateTeacher
            {
                SubjectSelectListItem = subjectSelectListItems,
                PageTitle = "Create Teacher"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(vmCreateTeacher model)
        {
            if (ModelState.IsValid)
            {
                if (model.SystemMessage == null)
                {
                    Teacher teacherModel = new Teacher
                    {
                        SubjectID = model.Teacher.SubjectID,
                        Name = model.Teacher.Name,
                        BirthDate = model.Teacher.BirthDate,
                        Salary = model.Teacher.Salary
                    };

                    _unitOfWork.TeacherRepository.Add(teacherModel);
                    _unitOfWork.Save();

                    model.PageTitle = "Create Teacher";
                    return RedirectToAction("List");
                }
            }

            if (string.IsNullOrEmpty(model.Teacher.Name))
            {
                model.SystemMessage = new SystemMessage
                {
                    MessageText = "please enter the name",
                    success = false
                };
            }

            model.SubjectSelectListItem = (List<SubjectListItem>)TempData["subjectSelectListItems"];
            TempData["subjectSelectListItems"] = model.SubjectSelectListItem;
            model.PageTitle = "Create Teacher";
            return View(model);
        }
        public ActionResult List(int page = 1, bool showError = false)
        {
            var teacherPagedList = _unitOfWork.TeacherRepository.GetTeacherPagedList(page, 30);

            vmTeacherPagedList model = new vmTeacherPagedList
            {
                TeacherPagedList = teacherPagedList,
                PageTitle = "Teacher List",
            };

            if (showError)
            {
                model.SystemMessage = new SystemMessage
                {
                    MessageText = "you can not delete the teacher becuase it is used in other entities",
                    success = false
                };
            }

            model.Pager = new Pager
            {
                CurrentPage = teacherPagedList.CurrentPage,
                TotalPage = teacherPagedList.TotalPages,
                TotalCount = teacherPagedList.TotalCount,
                ActionUrl = "List"
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult List(int? itemForDelete, int page = 1)
        {
            if (itemForDelete.HasValue)
            {
                //if (!_unitOfWork.TeacherRepository.HasRelationship((int)itemForDelete))
                //{
                    _unitOfWork.TeacherRepository.Delete((int)itemForDelete);
                    _unitOfWork.Save();
                //}
                //else
                //{
                  //  return List(page, true);
                //}
            }
            return List(page);
        }
        public ActionResult Detail(string id)
        {
            int selectedID;
            if (!int.TryParse(id, out selectedID))
                return RedirectToAction("List");

            var teacherDetails = _unitOfWork.TeacherRepository.GetTeacherDetails(selectedID);
            if (teacherDetails == null)
                return RedirectToAction("List");

            var subjectSelectListItems = _unitOfWork.SubjectRepository.GetSubjectSelectListItem();
            TempData["subjectSelectListItems"] = subjectSelectListItems;

            vmTeacherDetails model = new vmTeacherDetails()
            {
                SubjectSelectListItem = subjectSelectListItems,
                Teacher = teacherDetails,
                PageTitle = "Teacher Detail"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(vmTeacherDetails model, string id)
        {
            int selectedID;
            if (!int.TryParse(id, out selectedID))
                return RedirectToAction("List");

            var teacherDetailModel = _unitOfWork.TeacherRepository.GetTeacherDetails(selectedID);
            if (teacherDetailModel == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                teacherDetailModel.SubjectID = model.Teacher.SubjectID;
                teacherDetailModel.Name = model.Teacher.Name;
                teacherDetailModel.BirthDate = model.Teacher.BirthDate;
                teacherDetailModel.Salary = model.Teacher.Salary;

                _unitOfWork.TeacherRepository.Edit(teacherDetailModel);
                _unitOfWork.Save();

                model.SystemMessage = new SystemMessage
                {
                    MessageText = "Information updated successfully",
                    success = true
                };
            }
            else
            {
                if (string.IsNullOrEmpty(model.Teacher.Name))
                {
                    model.SystemMessage = new SystemMessage
                    {
                        MessageText = "please enter the name",
                        success = false
                    };
                }
            }

            var teacherDetail = _unitOfWork.TeacherRepository.GetTeacherDetails(selectedID);
            model.SubjectSelectListItem = (List<SubjectListItem>)TempData["subjectSelectListItems"];
            TempData["subjectSelectListItems"] = model.SubjectSelectListItem;
            teacherDetail.SubjectID = model.Teacher.SubjectID;
            teacherDetail.Name = model.Teacher.Name;
            teacherDetail.BirthDate = model.Teacher.BirthDate;
            teacherDetail.Salary = model.Teacher.Salary;
            model.Teacher = teacherDetail;
            model.PageTitle = "Subject Detail";
            return View(model);
        }
    }
}