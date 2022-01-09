using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmEnrollmentDetails : BaseModel
    {
        public Enrollment Enrollment { get; set; }
        public List<SubjectListItem> SubjectSelectListItem { get; set; }
        public List<StudentListItem> StudentSelectListItem { get; set; }
    }
}