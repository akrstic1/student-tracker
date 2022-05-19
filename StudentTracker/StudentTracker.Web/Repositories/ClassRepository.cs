using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentTracker.DAL;
using StudentTracker.Model;

namespace StudentTracker.Web.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly StudentTrackerDbContext _studentTrackerDbContext;

        public ClassRepository(StudentTrackerDbContext studentTrackerDbContext)
        {
            _studentTrackerDbContext = studentTrackerDbContext;
        }

        public async Task<IEnumerable<Class>> GetAllClasses()
        {
            return await _studentTrackerDbContext.Classes.ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetAllClassStudents()
        {
            return await _studentTrackerDbContext.Classes.Include(p => p.Students).ToListAsync();
        }

        public async Task<Class> GetClass(Guid id)
        {
            return await _studentTrackerDbContext.Classes
                .Include(p => p.Students)
                .ThenInclude(p => p.Student)
                .FirstOrDefaultAsync(p => p.ClassId == id);
        }

        public async Task AddClass(Class classAdd)
        {
            await _studentTrackerDbContext.Classes.AddAsync(classAdd);

            await SaveChanges();
        }

        public async Task DeleteClass(Class classDelete)
        {
            _studentTrackerDbContext.Classes.Remove(classDelete);

            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _studentTrackerDbContext.SaveChangesAsync();
        }
    }
}