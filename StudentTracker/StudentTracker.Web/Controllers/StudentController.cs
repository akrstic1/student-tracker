using Microsoft.AspNetCore.Mvc;
using StudentTracker.Model;
using StudentTracker.Web.Models;
using StudentTracker.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTracker.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("student/index")]
        public async Task<IActionResult> Index(StudentFilterModel filter)
        {
            IEnumerable<Student> studentQuery = await _studentRepository.GetAll();

            filter = filter ?? new StudentFilterModel();

            if (!string.IsNullOrWhiteSpace(filter.FullName))
                studentQuery = studentQuery.Where(p => (p.FullName).ToLower().Contains(filter.FullName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Address))
                studentQuery = studentQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Phone))
                studentQuery = studentQuery.Where(p => p.Phone.ToLower().Contains(filter.Phone.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Jmbag))
                studentQuery = studentQuery.Where(p => p.Jmbag.ToLower().Contains(filter.Jmbag.ToLower()));
          
            List<Student> model = studentQuery.ToList();
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexFilter(StudentFilterModel filter)
        {
            IEnumerable<Student> studentQuery = await _studentRepository.GetAll();

            filter = filter ?? new StudentFilterModel();

            if (!string.IsNullOrWhiteSpace(filter.FullName))
                studentQuery = studentQuery.Where(p => (p.FullName).ToLower().Contains(filter.FullName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Address))
                studentQuery = studentQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Phone))
                studentQuery = studentQuery.Where(p => p.Phone.ToLower().Contains(filter.Phone.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Jmbag))
                studentQuery = studentQuery.Where(p => p.Jmbag.ToLower().Contains(filter.Jmbag.ToLower()));

            List<Student> model = studentQuery.ToList();
            return PartialView("_IndexTable", model);
        }

        [HttpGet("student/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model)
        {
            if (ModelState.IsValid)
            {
                await _studentRepository.Add(model);
                await _studentRepository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet("student/edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Student model = await _studentRepository.GetStudent(id);

            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(Guid studentId)
        {
            Student model = await _studentRepository.GetStudent(studentId);
            var ok = await TryUpdateModelAsync(model);

            if (ok && this.ModelState.IsValid)
            {
                await _studentRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet("student/details")]
        public async Task<IActionResult> Details(Guid studentId)
        {
            var studentProfile = await _studentRepository.GetStudent(studentId);

            if (studentProfile != null)
            {
                return View(studentProfile);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Remove(Guid id)
        {
            var studentToDelete = await _studentRepository.GetStudent(id);

            if (studentToDelete != null)
            {
                await _studentRepository.Remove(studentToDelete);

                return RedirectToAction("Index");
            }

            return NotFound();

        }
    }
}