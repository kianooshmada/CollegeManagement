using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.ViewModels
{
    public class SubjectListReport
    {
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Salary { get; set; }
        public int? StudentsCount { get; set; }
        public double? GradeAvg { get; set; }
    }
}
