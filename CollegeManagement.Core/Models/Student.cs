using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CollegeManagement.Core.Models
{
    public class Student: IEntity
    {
        public int StudentID { get; set; }
        [Required, MaxLength(16)]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
