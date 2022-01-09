using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmSubjectDetails: BaseModel
    {
        public Subject Subject { get; set; }
        public List<CourseListItem> CourseSelectListItem { get; set; }
    }
}