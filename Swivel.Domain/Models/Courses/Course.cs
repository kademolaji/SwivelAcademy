using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Swivel.Domain.Models
{
    [Table("course")]
    public class Course 
    {
        public Course()
        {
            StudentCourse = new HashSet<StudentCourse>();
            TeacherCourse = new HashSet<TeacherCourse>();
        }
        [Key]
        [Column("CourseId")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }
        public ICollection<TeacherCourse> TeacherCourse { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
