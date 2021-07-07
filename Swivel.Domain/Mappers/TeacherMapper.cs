using Swivel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swivel.Domain.Mappers
{
    public static class TeacherMapper
    {
        public static void Map(this Teacher dbTeacher, Teacher teacher)
        {
            dbTeacher.FirstName = teacher.FirstName;
            dbTeacher.LastName = teacher.LastName;
            dbTeacher.Email = teacher.Email;
            dbTeacher.Address = teacher.Address;
            dbTeacher.DateOfBirth = teacher.DateOfBirth;
        }
    }
}
