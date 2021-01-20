using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkolaGitareAPI.Data;
using SkolaGitareAPI.Data.Entities;
using SkolaGitareAPI.Data.Repositories.Interfaces;
using SkolaGitareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkolaGitareAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Teacher, Student")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IPersonRepository personRepository;
        private readonly IAppointmentRepository repository;
        private readonly UserManager<Person> userManager;
        private readonly ILogger<AppointmentsController> logger;

        public AppointmentsController(IPersonRepository personRepository, IAppointmentRepository repository, UserManager<Person> userManager, ILogger<AppointmentsController> logger)
        {
            this.personRepository = personRepository;
            this.repository = repository;
            this.userManager = userManager;
            this.logger = logger;
        }



        // GET: api/<Appointments>
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetAppointmentsWithStudents());
        }

        // GET api/<Appointments>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            var app = await repository.Get(id);

            if (app == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(app);
            }
        }

        // POST api/<Appointments>
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Post([FromBody] CreateAppointmentModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create appointment" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create appointment" });
            }

            string teacherId = userManager.GetUserId(HttpContext.User);

            var teacher = await personRepository.Get(teacherId);

            if (teacher == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create appointment" });
            }

            logger.LogInformation("Attempting to create an appointment on " + model.DateTimeStart + " with duration " + model.Duration);

            var id = await repository.CreateAppointment(model.DateTimeStart, model.Duration, teacher, model.StudentsIds.ToList());

            if (id == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create appointment" });
            }

            return Ok(id);
        }

        // PUT api/<Appointments>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateAppointmentModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment" });
            }

            logger.LogInformation("Attempting an appointment with the following id: " + id);

            var app = await repository.Get(id);

            if (app == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment" });
            }

            app.Duration = model.Duration;
            app.DateTimeStart = model.DateTimeStart;

            var result = await repository.Update(id, app);

            if (result)
            {
                logger.LogInformation("Successfully updated an appointment with the following id: " + app.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment" });
            }
        }

        [Produces("application/json")]
        [Authorize(Roles = "Teacher")]
        [HttpPut("{id}/Duration")]
        public async Task<IActionResult> UpdateAppointmentDuration(UpdateAppointmentTimeModel model)
        {

            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment" });
            }

            logger.LogInformation("Attempting to update time for an appointment with the following id: " + model.AppointmentId);

            var app = await repository.Get(model.AppointmentId);

            if (app == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment" });
            }

            app.Duration = model.Duration;

            var result = await repository.Update(model.AppointmentId, app);

            if (result)
            {
                logger.LogInformation("Successfully updated time for an appointment with the following id: " + app.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment" });
            }
        }

        // DELETE api/<Appointments>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(Guid id)
        {

            logger.LogInformation("Attempting to delete an appointment with the following id: " + id);

            var app = await repository.Get(id);

            if (app == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to delete appointment" });
            }

            var result = await repository.Delete(id);

            if (result)
            {
                logger.LogInformation("Successfully deleted an appointment with the following id: " + app.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to delete appointment" });
            }
        }

        [HttpGet("{id}/Students")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetStudents(Guid? id)
        {
            if (id == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to get students" });
            }

            return Ok(await repository.GetStudents(id.Value));
        }

        [HttpGet("Student/{id}")]
        public async Task<IActionResult> GetStudentsAppointments(string id)
        {
            if (id == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to get students" });
            }

            return Ok(await repository.GetStudentAppointments(id));
        }

        [HttpPut("{id}/Students/Add")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddStudents(Guid id, IEnumerable<string> studentIds)
        {
            if (studentIds == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to add students" });
            }

            if (studentIds.Count() == 0)
            {
                return Ok();
            }


            if (await repository.AddStudents(id, studentIds))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to add students" });
            }
        }

        [HttpPut("{id}/Students/Remove")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> RemoveStudents(Guid id, IEnumerable<string> studentIds)
        {
            if (studentIds == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to remove students" });
            }

            if (studentIds.Count() == 0)
            {
                return Ok();
            }


            if (await repository.RemoveStudents(id, studentIds))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to remove students" });
            }
        }
    }
}
