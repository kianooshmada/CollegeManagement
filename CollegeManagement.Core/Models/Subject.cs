using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CollegeManagement.Core.Models
{
    public class Subject : IEntity
    {
        public int SubjectID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required, MaxLength(16)]
        public string Name { get; set; }
        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
