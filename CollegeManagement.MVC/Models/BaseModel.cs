using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class BaseModel
    {
        public string PageTitle { get; set; }
        public SystemMessage SystemMessage { get; set; }
    }
}