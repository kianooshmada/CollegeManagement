using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.ViewModels
{
    public class StudentListReport
    {
        public string SubjectName { get; set; }
        public string StudentName { get; set; }
        public int? RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public double? Grade { get; set; }
    }
}
