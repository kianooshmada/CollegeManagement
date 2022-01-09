using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.ViewModels
{
    public class CourseListReport
    {
        public string Name { get; set; }
        public int TeachersCount { get; set; }
        public int StudentsCount { get; set; }
        public double? GradeAvg { get; set; }
    }
}
