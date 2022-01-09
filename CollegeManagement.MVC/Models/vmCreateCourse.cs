using CollegeManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmCreateCourse: BaseModel
    {
        public Course Course { get; set; }
    }
}