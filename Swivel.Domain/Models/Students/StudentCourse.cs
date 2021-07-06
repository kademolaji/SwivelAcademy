using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Swivel.Domain.Models
{
    [Table("studentcourse")]
    public class StudentCourse
    {
        [Required(ErrorMessage = "StudentId is required")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "CourseId is required")]
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

    }
}
