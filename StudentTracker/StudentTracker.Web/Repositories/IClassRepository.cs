using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentTracker.Model;

namespace StudentTracker.Web.Repositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllClasses();
        Task<IEnumerable<Class>> GetAllClassStudents();
        Task<Class> GetClass(Guid id);
        Task AddClass(Class classAdd);
        Task DeleteClass(Class classDelete);
        Task SaveChanges();
    }
}