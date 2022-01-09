using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CollegeManagement.Core.Models
{
    public class Teacher : IEntity
    {
        public int SubjectID { get; set; }
        [Required, MaxLength(16)]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public decimal Salary { get; set; }
    }
}
