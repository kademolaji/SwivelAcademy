using Swivel.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swivel.Repository
{
    public interface IRepositoryWrapper
    {
        ITeacherRepository Teacher { get; }
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _dbContext;
        private ITeacherRepository _teacher;

        public ITeacherRepository Teacher
        {
            get
            {
                if (_teacher == null)
                {
                    _teacher = new TeacherRepository(_dbContext);
                }

                return _teacher;
            }
        }

        public RepositoryWrapper(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
