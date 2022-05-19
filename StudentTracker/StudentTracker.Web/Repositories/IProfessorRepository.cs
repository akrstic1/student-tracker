using System.Collections.Generic;
using System.Threading.Tasks;
using StudentTracker.Model;

namespace StudentTracker.Web.Repositories
{
    public interface IProfessorRepository
    {
        Task<IEnumerable<Professor>> GetAll();
        Task<IEnumerable<Professor>> GetAllApi();
        Task SaveChanges();
    }
}