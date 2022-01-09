using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmCreateTeacher : BaseModel
    {
        public Teacher Teacher { get; set; }
        public List<SubjectListItem> SubjectSelectListItem { get; set; }
    }
}