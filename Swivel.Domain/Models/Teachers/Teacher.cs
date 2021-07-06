using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Swivel.Domain.Models
{
    [Table("teacher")]
    public class Teacher
    {
        public Teacher()
        {
            TeacherCourse = new HashSet<TeacherCourse>();
        }
        [Key]
        [Column("TeacherId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(60, ErrorMessage = "First Name can't be longer than 60 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(60, ErrorMessage = "Last Name can't be longer than 60 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can not be longer then 100 characters")]
        public string Address { get; set; }
        public ICollection<TeacherCourse> TeacherCourse { get; set; }
    }
}
