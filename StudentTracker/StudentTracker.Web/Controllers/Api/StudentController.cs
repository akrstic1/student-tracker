using Microsoft.AspNetCore.Mvc;
using StudentTracker.Model;
using StudentTracker.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StudentTracker.Web.Models;

namespace StudentTracker.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Get all students.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var students = (await _studentRepository.GetAll())
                .Select(p => new StudentDto
                {
                    StudentId = p.StudentId,
                    FullName = p.FullName,
                    Address = p.Address,
                    Phone = p.Phone,
                    Jmbag = p.Jmbag
                });

            return students.Any() ?
                Ok(students) :
                NotFound();
        }

        /// <summary>
        /// Return a single student by id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> Get(Guid id)
        {
            var student = (await _studentRepository.GetAll())
                .Where(p => p.StudentId == id)
                .Select(p => new StudentDto
                {
                    StudentId = p.StudentId,
                    FullName = p.FullName,
                    Address = p.Address,
                    Phone = p.Phone,
                    Jmbag = p.Jmbag
                })
                .SingleOrDefault();

            return student != default ?
                Ok(student) :
                NotFound();
        }

        /// <summary>
        /// Add a new student and return it.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> Post([FromBody] StudentDto student)
        {
            var studentDb = new Student
            {
                StudentId = Guid.NewGuid(),
                FullName = student.FullName,
                Address = student.Address,
                Phone = student.Phone,
                Jmbag = student.Jmbag
            };

            await _studentRepository.Add(studentDb);

            return await Get(studentDb.StudentId);
        }

        /// <summary>
        /// Update a student and return it.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> Put(Guid id, [FromBody] StudentDto student)
        {
            var studentToUpdate = (await _studentRepository.GetAll()).SingleOrDefault(p => p.StudentId == id);

            if (studentToUpdate == default)
                return NotFound();

            studentToUpdate.FullName = student.FullName;
            studentToUpdate.Address = student.Address;
            studentToUpdate.Phone = student.Phone;
            studentToUpdate.Jmbag = student.Jmbag;

            await _studentRepository.SaveChanges();

            return await Get(id);
        }

        /// <summary>
        /// Delete a student and return all students.
        /// </summary>
        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentDto>>> Delete(Guid id)
        {
            var studentToDelete = (await _studentRepository.GetAll()).SingleOrDefault(p => p.StudentId == id);

            if (studentToDelete == default)
                return NotFound();
            
            await _studentRepository.Remove(studentToDelete);

            return await GetAll();
        }
    }
}