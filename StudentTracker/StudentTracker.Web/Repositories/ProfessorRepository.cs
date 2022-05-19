using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentTracker.DAL;
using StudentTracker.Model;

namespace StudentTracker.Web.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly StudentTrackerDbContext _studentTrackerDbContext;

        public ProfessorRepository(StudentTrackerDbContext studentTrackerDbContext)
        {
            _studentTrackerDbContext = studentTrackerDbContext;
        }

        public async Task<IEnumerable<Professor>> GetAll()
        {
            return await _studentTrackerDbContext.Professors
                .Include(professor => professor.Classes)
                .ToListAsync();
        }

        public async Task<IEnumerable<Professor>> GetAllApi()
        {
            return await _studentTrackerDbContext.Professors
                .ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _studentTrackerDbContext.SaveChangesAsync();
        }
    }
}