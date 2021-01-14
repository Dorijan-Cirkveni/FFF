using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkolaGitareAPI.Data.Entities;
using SkolaGitareAPI.Data.Repositories.Interfaces;
using SkolaGitareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkolaGitareAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Teacher")]
    [ApiController]
    public class LessonsController : ControllerBase
    {

        private readonly ILessonRepository repository;
        private readonly IPersonRepository personRepository;
        private readonly UserManager<Person> userManager;
        private readonly ILogger<LessonsController> logger;

        public LessonsController(   ILessonRepository repository, 
                                    UserManager<Person> userManager, 
                                    ILogger<LessonsController> logger, 
                                    IPersonRepository personRepository)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.logger = logger;
            this.personRepository = personRepository;
        }

        // GET: api/<LessonsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // GET api/<LessonsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            var lesson = await repository.Get(id);

            if (lesson == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(lesson);
            }
        }

        // POST api/<LessonsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLessonModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create lesson" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create lesson" });
            }

            string teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var teacher = await personRepository.Get(teacherId);

            if (teacher == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create lesson" });
            }

            var student = await personRepository.Get(model.For);

            if (student == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create lesson" });
            }

            logger.LogInformation("Attempting to create an lesson");

            Lesson lesson = new Lesson
            {
                DownloadLink = model.DownloadLink,
                From = teacher,
                For = student,
                Id = Guid.NewGuid(),
                LessonDate = DateTime.Now,
                Name = model.Name,
                Notes = model.Notes
            };

            var result = await repository.Create(lesson);

            if (!result)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create appointment" });
            }

            return Ok(lesson.Id);
        }

        // PUT api/<LessonsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateLessonModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update lesson" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update lesson" });
            }

            logger.LogInformation("Attempting to update an lesson with the following id: " + id);

            var lesson = await repository.Get(id);

            if (lesson == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update lesson" });
            }

            lesson.DownloadLink = model.DownloadLink;
            lesson.Name = model.Name;
            lesson.Notes = model.Notes;

            var result = await repository.Update(id, lesson);

            if (result)
            {
                logger.LogInformation("Successfully updated an lesson with the following id: " + lesson.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to update lesson" });
            }
        }

        // DELETE api/<LessonsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation("Attempting to delete an lesson with the following id: " + id);

            var app = await repository.Get(id);

            if (app == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to delete lesson" });
            }

            var result = await repository.Delete(id);

            if (result)
            {
                logger.LogInformation("Successfully deleted an lesson with the following id: " + app.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to delete lesson" });
            }
        }

        [HttpGet("Student/{id}")]
        public async Task<IActionResult> GetStudentLessons(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var lesson = await repository.GetStudentLessons(id);

            if (lesson == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(lesson);
            }
        }

        [HttpGet("NextAppointment")]
        public async Task<IActionResult> GetStudentLessonsForNextAppointment(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var lesson = await repository.GetEarliestLessonForStudentsInNextAppointment();

            if (lesson == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(lesson);
            }
        }
    }
}
