using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmStudentListReport : BaseModel
    {
        public PagedList<StudentListReport> StudentList { get; set; }
        public Pager Pager { get; set; }
    }
}