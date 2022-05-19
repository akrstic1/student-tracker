using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentTracker.DAL;
using StudentTracker.Model;

namespace StudentTracker.Web.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentTrackerDbContext _studentTrackerDbContext;

        public StudentRepository(StudentTrackerDbContext studentTrackerDbContext)
        {
            _studentTrackerDbContext = studentTrackerDbContext;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _studentTrackerDbContext.Students.ToArrayAsync();
        }

        public async Task<Student> GetStudent(Guid studentId)
        {
            return await _studentTrackerDbContext.Students
                .Include(p => p.Classes)
                .ThenInclude(p => p.Class)
                .FirstOrDefaultAsync(p => p.StudentId == studentId);
        }

        public async Task Add(Student student)
        {
            await _studentTrackerDbContext.Students.AddAsync(student);

            await SaveChanges();
        }

        public async Task Remove(Student student)
        {
            _studentTrackerDbContext.Students.Remove(student);

            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _studentTrackerDbContext.SaveChangesAsync();
        }
    }
}