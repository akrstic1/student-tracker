using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using StudentTracker.Web.Repositories;

namespace StudentTracker.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ProfessorController : Controller
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorController(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        // TODO: Add auth and stop using a random professor.
        public async Task<IActionResult> Index()
        {
            var userProfile = (await _professorRepository.GetAll()).FirstOrDefault();

            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> GetForm(Guid id)
        {
            var userProfile = (await _professorRepository.GetAll())
                .Single(professor => professor.ProfessorId == id);

            return PartialView("_EditUser", userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(Guid id)
        {
            var userProfile = (await _professorRepository.GetAll())
                .Single(professor => professor.ProfessorId == id);
            var updateSuccess = await TryUpdateModelAsync(userProfile);

            if (updateSuccess && ModelState.IsValid)
                await _professorRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}