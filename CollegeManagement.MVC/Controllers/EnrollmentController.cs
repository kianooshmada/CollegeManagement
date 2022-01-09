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
    public class EnrollmentController : BaseController
    {
        // GET: Enrollment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var subjectSelectListItems = _unitOfWork.SubjectRepository.GetSubjectSelectListItem();
            TempData["subjectSelectListItems"] = subjectSelectListItems;

            var studentSelectListItems = _unitOfWork.StudentRepository.GetStudentSelectListItem();
            TempData["studentSelectListItems"] = studentSelectListItems;

            vmCreateEnrollment model = new vmCreateEnrollment
            {
                SubjectSelectListItem = subjectSelectListItems,
                StudentSelectListItem = studentSelectListItems,
                PageTitle = "Create Enrollment"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(vmCreateEnrollment model)
        {
            if (ModelState.IsValid)
            {
                if (model.SystemMessage == null)
                {
                    Enrollment enrollmentModel = new Enrollment
                    {
                        SubjectID = model.Enrollment.SubjectID,
                        StudentID = model.Enrollment.StudentID,
                        Grade = model.Enrollment.Grade,
                        RegistrationDate = model.Enrollment.RegistrationDate,
                        RegistrationNumber = model.Enrollment.RegistrationNumber,
                    };

                    _unitOfWork.EnrollmentRepository.Add(enrollmentModel);
                    _unitOfWork.Save();

                    model.PageTitle = "Create Enrollment";
                    return RedirectToAction("List");
                }
            }

            model.SubjectSelectListItem = (List<SubjectListItem>)TempData["subjectSelectListItems"];
            TempData["courseSelectListItems"] = model.SubjectSelectListItem;

            model.StudentSelectListItem = (List<StudentListItem>)TempData["studentSelectListItems"];
            TempData["studentSelectListItems"] = model.StudentSelectListItem;

            model.PageTitle = "Create Enrollment";
            return View(model);
        }

        public ActionResult List(int page = 1, bool showError = false)
        {
            var enrollmentPagedList = _unitOfWork.EnrollmentRepository.GetEnrollmentPagedList(page, 30);

            vmEnrollmentPagedList model = new vmEnrollmentPagedList
            {
                EnrollmentPagedList = enrollmentPagedList,
                PageTitle = "Enrollment List",
            };

            if (showError)
            {
                model.SystemMessage = new SystemMessage
                {
                    MessageText = "you can not delete the enrollment becuase it is used in other entities",
                    success = false
                };
            }

            model.Pager = new Pager
            {
                CurrentPage = enrollmentPagedList.CurrentPage,
                TotalPage = enrollmentPagedList.TotalPages,
                TotalCount = enrollmentPagedList.TotalCount,
                ActionUrl = "List"
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult List(int? itemForDelete, int page = 1)
        {
            if (itemForDelete.HasValue)
            {
                _unitOfWork.EnrollmentRepository.Delete((int)itemForDelete);
                _unitOfWork.Save();
            }
            return List(page);
        }
        public ActionResult Detail(string id)
        {
            int selectedID;
            if (!int.TryParse(id, out selectedID))
                return RedirectToAction("List");

            var enrollmengtDetails = _unitOfWork.EnrollmentRepository.GetEnrollmentDetails(selectedID);
            if (enrollmengtDetails == null)
                return RedirectToAction("List");

            var subjectSelectListItems = _unitOfWork.SubjectRepository.GetSubjectSelectListItem();
            TempData["subjectSelectListItems"] = subjectSelectListItems;

            var studentSelectListItems = _unitOfWork.StudentRepository.GetStudentSelectListItem();
            TempData["studentSelectListItems"] = studentSelectListItems;

            vmEnrollmentDetails model = new vmEnrollmentDetails()
            {
                SubjectSelectListItem = subjectSelectListItems,
                StudentSelectListItem = studentSelectListItems,
                Enrollment = enrollmengtDetails,
                PageTitle = "Enrollment Detail"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(vmEnrollmentDetails model, string id)
        {
            int selectedID;
            if (!int.TryParse(id, out selectedID))
                return RedirectToAction("List");

            var enrollmengtDetailModel = _unitOfWork.EnrollmentRepository.GetEnrollmentDetails(selectedID);
            if (enrollmengtDetailModel == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                enrollmengtDetailModel.EnrollmentID = selectedID;
                enrollmengtDetailModel.SubjectID = model.Enrollment.SubjectID;
                enrollmengtDetailModel.StudentID = model.Enrollment.StudentID;
                enrollmengtDetailModel.RegistrationDate = model.Enrollment.RegistrationDate;
                enrollmengtDetailModel.RegistrationNumber = model.Enrollment.RegistrationNumber;
                enrollmengtDetailModel.Grade = model.Enrollment.Grade;
                
                _unitOfWork.EnrollmentRepository.Edit(enrollmengtDetailModel);
                _unitOfWork.Save();

                model.SystemMessage = new SystemMessage
                {
                    MessageText = "Information updated successfully",
                    success = true
                };
            }
            else
            {
                //if (string.IsNullOrEmpty(model.Subject.Name))
                //{
                //    model.SystemMessage = new SystemMessage
                //    {
                //        MessageText = "please enter the name",
                //        success = false
                //    };
                //}
            }

            var enrollmenrDetail = _unitOfWork.EnrollmentRepository.GetEnrollmentDetails(selectedID);

            model.SubjectSelectListItem = (List<SubjectListItem>)TempData["subjectSelectListItems"];
            TempData["courseSelectListItems"] = model.SubjectSelectListItem;

            model.StudentSelectListItem = (List<StudentListItem>)TempData["studentSelectListItems"];
            TempData["studentSelectListItems"] = model.StudentSelectListItem;

            enrollmenrDetail.SubjectID = model.Enrollment.SubjectID;
            enrollmenrDetail.StudentID = model.Enrollment.StudentID;
            enrollmenrDetail.RegistrationNumber = model.Enrollment.RegistrationNumber;
            enrollmenrDetail.RegistrationDate = model.Enrollment.RegistrationDate;
            enrollmenrDetail.Grade = model.Enrollment.Grade;

            model.Enrollment = enrollmenrDetail;
            model.PageTitle = "Enrollment Detail";
            return View(model);
        }
    }
}