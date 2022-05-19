using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using StudentTracker.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Text.Json;
using StudentTracker.Model;

namespace StudentTracker.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ClassController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;

        public ClassController(IClassRepository classRepository, IStudentRepository studentRepository)
        {
            _classRepository = classRepository;
            _studentRepository = studentRepository;
        }

        // TODO: Add auth and stop using a random professor.
        [HttpGet("/user/classes")]
        public async Task<IActionResult> Index(Guid professorId)
        {
            var userClass = (await _classRepository.GetAllClassStudents())
                .Where(c => c.ProfessorId == professorId)
                .ToList();

            ViewBag.professorId = professorId;

            return View(userClass);
        }

        [HttpGet("/user/classes/edit")]
        public async Task<IActionResult> EditClass(Guid classId)
        {
            var userClass = await _classRepository.GetClass(classId);

            await FillDropdownValues(userClass);

            return View(userClass);
        }

        [HttpGet("user/classes/add")]
        public async Task<IActionResult> AddClass(Guid professorId)
        {
            await FillDropdownValues();

            ViewBag.professorId = professorId;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteClass(Guid classId)
        {
            var deleteClass = await _classRepository.GetClass(classId);

            await _classRepository.DeleteClass(deleteClass);

            return RedirectToAction("Index", new {professorId = deleteClass.ProfessorId});
        }

        [HttpGet]
        private async Task FillDropdownValues(Class userClass = null)
        {
            var selectItems = new List<SelectListItem>
            {
                new()
                {
                    Text = "- Odaberite -",
                    Value = ""
                }
            };

            var students = await _studentRepository.GetAll();

            if (userClass != null)
            {
                var studentsToErase = userClass.Students.Select(classStudent => classStudent.Student);
                students = students.Except(studentsToErase);
            }

            selectItems.AddRange(students.Select(student => new SelectListItem
            {
                Text = student.FullName,
                Value = student.StudentId.ToString()
            }));

            ViewBag.PossibleStudents = selectItems;
        }

        [HttpPost]
        public async Task<IActionResult> ApplyEditToClass(Guid classId, string studentsToEditJson)
        {
            var classToUpdate = await _classRepository.GetClass(classId);

            if (!string.IsNullOrEmpty(studentsToEditJson))
            {
                var studentIdsToEdit = JsonSerializer.Deserialize<Guid[]>(studentsToEditJson);
                var studentsToEdit = (await _studentRepository.GetAll())
                    .Where(student => studentIdsToEdit.Contains(student.StudentId));

                foreach (var student in studentsToEdit)
                {
                    if (classToUpdate.Students.Any(p => p.StudentId == student.StudentId))
                    {
                        var toDelete = classToUpdate.Students.First(p => p.StudentId == student.StudentId);
                        classToUpdate.Students.Remove(toDelete);
                    }
                    else
                    {
                        classToUpdate.Students.Add(new ClassStudent
                        {
                            ClassStudentId = new Guid(),
                            StudentId = student.StudentId,
                            Student = student,
                            ClassId = classToUpdate.ClassId,
                            Class = classToUpdate,
                            EnrollDate = DateTime.Now,
                            Pass = 0
                        });
                    }
                }
            }

            var updateSuccess = await TryUpdateModelAsync(classToUpdate);
            if (updateSuccess && ModelState.IsValid)
                await _classRepository.SaveChanges();

            return RedirectToAction("Index", new {professorId = classToUpdate.ProfessorId});
        }

        [HttpPost]
        public async Task<IActionResult> AddClassPost(Class model, string studentsToEditJson)
        {
            model.ClassId = Guid.NewGuid();

            await _classRepository.AddClass(model);

            var classToUpdate = await _classRepository.GetClass(model.ClassId);

            if (!string.IsNullOrEmpty(studentsToEditJson))
            {
                var studentIdsToEdit = JsonSerializer.Deserialize<Guid[]>(studentsToEditJson);
                var studentsToEdit = (await _studentRepository.GetAll())
                    .Where(student => studentIdsToEdit.Contains(student.StudentId));

                foreach (var student in studentsToEdit)
                {
                    classToUpdate.Students.Add(new ClassStudent
                    {
                        ClassStudentId = new Guid(),
                        StudentId = student.StudentId,
                        Student = student,
                        ClassId = model.ClassId,
                        Class = model,
                        EnrollDate = DateTime.Now,
                        Pass = 0
                    });
                }

                var updateSuccess = await TryUpdateModelAsync(classToUpdate);
                if (updateSuccess && ModelState.IsValid)
                    await _classRepository.SaveChanges();
            }

            return RedirectToAction("Index", new {professorId = model.ProfessorId});
        }

        [HttpGet]
        public IActionResult StudentLink(Guid studentId)
        {
            return PartialView("_StudentLink", studentId);
        }
    }
}