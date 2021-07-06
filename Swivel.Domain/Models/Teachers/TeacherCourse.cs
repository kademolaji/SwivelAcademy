using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Swivel.Domain.Models
{
    [Table("teachercourse")]
    public class TeacherCourse
    {
        [Required(ErrorMessage = "TeacherId is required")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "CourseId is required")]
        public int CourseId { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }

    }
}
