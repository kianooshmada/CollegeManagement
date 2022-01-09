using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CollegeManagement.Core.Models
{
    public class Enrollment :IEntity
    {
        public int EnrollmentID { get; set; }
        [Required]
        public int RegistrationNumber { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public int SubjectID { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        public double Grade { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
