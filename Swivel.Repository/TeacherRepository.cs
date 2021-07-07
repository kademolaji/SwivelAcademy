using Swivel.Database;
using Swivel.Domain.Mappers;
using Swivel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swivel.Repository
{
    public interface ITeacherRepository : IRepositoryBase<Teacher>
    {
        IEnumerable<Teacher> GetAllTeachers();
        Teacher GetTeacherById(int teacherId);
        void CreateTeacher(Teacher teacher);
        void UpdateTeacher(Teacher dbTeacher, Teacher teacher);
        void DeleteTeacher(Teacher teacher);
    }

    public class TeacherRepository : RepositoryBase<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return FindAll()
                .OrderBy(t => t.FirstName).ThenBy(l=>l.LastName);
        }

        public Teacher GetTeacherById(int teacherId)
        {
            return FindByCondition(teacher => teacher.Id.Equals(teacherId))
              .DefaultIfEmpty(new Teacher())
              .FirstOrDefault();
        }

        public void CreateTeacher(Teacher teacher)
        {
            Create(teacher);
            Save();
        }

        public void UpdateTeacher(Teacher dbTeacher, Teacher teacher)
        {
            dbTeacher.Map(teacher);
            Update(dbTeacher);
            Save();
        }

        public void DeleteTeacher(Teacher teacher)
        {
            Delete(teacher);
            Save();
        }
    }
}
