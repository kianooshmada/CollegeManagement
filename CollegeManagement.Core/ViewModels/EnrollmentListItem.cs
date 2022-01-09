using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.ViewModels
{
    public class EnrollmentListItem
    {
        public int EnrollmentID { get; set; }
        public int RegistrationNumber { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }        
        public DateTime RegistrationDate { get; set; }
        public double Grade { get; set; }
    }
}
