using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentTracker.Model;
using StudentTracker.Web.Repositories;

namespace StudentTracker.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorRepository _professorRepository;
        
        public ProfessorController(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }
        
        /// <summary>
        /// Get all professors.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Professor>>> GetAll()
        {
            var professors = await _professorRepository.GetAllApi();
            
            return professors.Any() ?
                Ok(professors) :
                NotFound();
        }
        
        /// <summary>
        /// Return a single professor by id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Professor>> GetProfessorById(Guid id)
        {
            var professorById = (await _professorRepository.GetAllApi())
                .SingleOrDefault(professor => professor.ProfessorId == id);

            return professorById != default ?
                Ok(professorById) :
                NotFound();
        }
    }
}