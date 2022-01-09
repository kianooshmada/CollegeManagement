using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CollegeManagement.Core.Models
{
    public class Course : IEntity
    {
        public int CourseID { get; set; }
        [Required, MaxLength(16)]
        public string Name { get; set; }        
        public List<Subject> Subjects { get; set; }
    }
}
