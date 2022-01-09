using CollegeManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class vmCreateStudent: BaseModel
    {
        public Student Student { get; set; }
    }
}