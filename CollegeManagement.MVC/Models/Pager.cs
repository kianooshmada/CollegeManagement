using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.MVC.Models
{
    public class Pager
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public string ActionUrl { get; set; }
    }
}