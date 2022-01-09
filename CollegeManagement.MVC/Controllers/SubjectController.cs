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
    public class SubjectController : BaseController
    {
        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var courseSelectListItems = _unitOfWork.CourseRepository.GetCourseSelectListItem();
            TempData["courseSelectListItems"] = courseSelectListItems;

            vmCreateSubject model = new vmCreateSubject
            {
                CourseSelectListItem = courseSelectListItems,
                PageTitle = "Create Subject"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(vmCreateSubject model)
        {
            if (ModelState.IsValid)
            {
                if (model.SystemMessage == null)
                {
                    Subject subjectModel = new Subject
                    {
                        CourseID = model.Subject.CourseID,
                        Name = model.Subject.Name
                    };

                    _unitOfWork.SubjectRepository.Add(subjectModel);
                    _unitOfWork.Save();

                    model.PageTitle = "Create Subject";
                    return RedirectToAction("List");
                }
            }

            if (string.IsNullOrEmpty(model.Subject.Name))
            {
                model.SystemMessage = new SystemMessage
                {
                    MessageText = "please enter the name",
                    success = false
                };
            }

            model.CourseSelectListItem = (List<CourseListItem>)TempData["courseSelectListItems"];
            TempData["courseSelectListItems"] = model.CourseSelectListItem;
            model.PageTitle = "Create Subject";
            return View(model);
        }
        public ActionResult List(int page = 1, bool showError = false)
        {
            var subjectPagedList = _unitOfWork.SubjectRepository.GetSubjectPagedList(page, 30);

            vmSubjectPagedList model = new vmSubjectPagedList
            {
                SubjectPagedList = subjectPagedList,
                PageTitle = "Subject List",
            };

            if (showError)
            {
                model.SystemMessage = new SystemMessage
                {
                    MessageText = "you can not delete the subject becuase it is used in other entities",
                    success = false
                };
            }

            model.Pager = new Pager
            {
                CurrentPage = subjectPagedList.CurrentPage,
                TotalPage = subjectPagedList.TotalPages,
                TotalCount = subjectPagedList.TotalCount,
                ActionUrl = "List"
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult List(int? itemForDelete, int page = 1)
        {
            if (itemForDelete.HasValue)
            {
                if (!_unitOfWork.SubjectRepository.HasRelationship((int)itemForDelete))
                {
                    _unitOfWork.SubjectRepository.Delete((int)itemForDelete);
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

            var subjectDetails = _unitOfWork.SubjectRepository.GetSubjectDetails(selectedID);
            if (subjectDetails == null)
                return RedirectToAction("List");

            var courseSelectListItems = _unitOfWork.CourseRepository.GetCourseSelectListItem();
            TempData["courseSelectListItems"] = courseSelectListItems;

            vmSubjectDetails model = new vmSubjectDetails()
            {
                CourseSelectListItem = courseSelectListItems,
                Subject = subjectDetails,
                PageTitle = "Subject Detail"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(vmSubjectDetails model, string id)
        {
            int selectedID;
            if (!int.TryParse(id, out selectedID))
                return RedirectToAction("List");

            var subjectDetails = _unitOfWork.SubjectRepository.GetSubjectDetails(selectedID);
            if (subjectDetails == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                subjectDetails.CourseID = model.Subject.CourseID;
                subjectDetails.Name = model.Subject.Name;
                _unitOfWork.SubjectRepository.Edit(subjectDetails);
                _unitOfWork.Save();

                model.SystemMessage = new SystemMessage
                {
                    MessageText = "Information updated successfully",
                    success = true
                };
            }
            else
            {
                if (string.IsNullOrEmpty(model.Subject.Name))
                {
                    model.SystemMessage = new SystemMessage
                    {
                        MessageText = "please enter the name",
                        success = false
                    };
                }
            }

            var subjectDetail = _unitOfWork.SubjectRepository.GetSubjectDetails(selectedID);
            model.CourseSelectListItem = (List<CourseListItem>)TempData["courseSelectListItems"];
            TempData["courseSelectListItems"] = model.CourseSelectListItem;
            subjectDetail.CourseID = model.Subject.CourseID;
            subjectDetail.Name = model.Subject.Name;
            model.Subject = subjectDetail;
            model.PageTitle = "Subject Detail";
            return View(model);
        }

        public ActionResult SubjectListReport(int page = 1)
        {
            var subjectListReport = _unitOfWork.SubjectRepository.GetSubjectReport(page, 30);

            vmSubjectListReport model = new vmSubjectListReport
            {
                SubjectList = subjectListReport,
                PageTitle = "Subject List",
            };
                        
            model.Pager = new Pager
            {
                CurrentPage = subjectListReport.CurrentPage,
                TotalPage = subjectListReport.TotalPages,
                TotalCount = subjectListReport.TotalCount,
                ActionUrl = "SubjectListReport"
            };

            return View(model);
        }
    }
}