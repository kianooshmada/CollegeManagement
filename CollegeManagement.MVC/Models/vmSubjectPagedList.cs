using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmSubjectPagedList : BaseModel
    {
        public PagedList<SubjectListItem> SubjectPagedList { get; set; }
        public Pager Pager { get; set; }
    }
}