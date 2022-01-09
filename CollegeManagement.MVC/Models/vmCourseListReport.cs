using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmCourseListReport : BaseModel
    {
        public PagedList<CourseListReport> CourseList { get; set; }
        public Pager Pager { get; set; }
    }
}