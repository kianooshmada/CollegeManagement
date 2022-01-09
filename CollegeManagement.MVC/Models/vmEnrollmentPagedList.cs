using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmEnrollmentPagedList : BaseModel
    {
        public PagedList<EnrollmentListItem> EnrollmentPagedList { get; set; }
        public Pager Pager { get; set; }
    }
}