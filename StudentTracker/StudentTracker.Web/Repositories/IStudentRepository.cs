using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentTracker.Model;

namespace StudentTracker.Web.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetStudent(Guid studentId);
        Task Add(Student student);
        Task Remove(Student student);
        Task SaveChanges();
    }
}