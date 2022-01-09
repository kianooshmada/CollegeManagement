using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmStudentPagedList : BaseModel
    {
        public PagedList<StudentListItem> StudentPagedList { get; set; }
        public Pager Pager { get; set; }
    }
}