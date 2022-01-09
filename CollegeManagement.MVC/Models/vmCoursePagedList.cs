using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmCoursePagedList : BaseModel
    {
        public PagedList<CourseListItem> CoursePagedList { get; set; }
        public Pager Pager { get; set; }
    }
}