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
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkolaGitareAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Teacher, Student, Admin")]
    [ApiController]
    public class AppointmentRequestsController : ControllerBase
    {
        private readonly IPersonRepository personRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IAppointmentRequestRepository repository;
        private readonly UserManager<Person> userManager;
        private readonly ILogger<AppointmentRequestsController> logger;

        public AppointmentRequestsController(IPersonRepository personRepository, IAppointmentRepository appointmentRepository, IAppointmentRequestRepository repository, UserManager<Person> userManager, ILogger<AppointmentRequestsController> logger)
        {
            this.personRepository = personRepository;
            this.appointmentRepository = appointmentRepository;
            this.repository = repository;
            this.userManager = userManager;
            this.logger = logger;
        }


        // GET: api/<AppointmentRequestsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetAppointmentRequestsWithUserInfo());
        }

        // GET api/<AppointmentRequestsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            var app = await repository.GetAppointmentRequestWithUserInfo(id);

            if (app == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(app);
            }
        }

        // POST api/<AppointmentRequestsController>
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Post([FromBody] CreateAppointmentRequestModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create appointment request" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create appointment request" });
            }

            string studentId = userManager.GetUserId(HttpContext.User);

            var student = await personRepository.Get(studentId);

            if (student == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create appointment request" });
            }

            logger.LogInformation("Attempting to create an appointment request on " + model.DateTimeStart);

            var app = new AppointmentRequest { DateTimeStart = model.DateTimeStart, Id = Guid.NewGuid(), RequestedBy = student };

            var result = await repository.Create(app);

            if (!result)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create appointment" });
            }

            return Ok(app.Id);
        }

        // PUT api/<AppointmentRequestsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateAppointmentRequestModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment request" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment request" });
            }

            logger.LogInformation("Attempting an appointment request with the following id: " + id);

            var app = await repository.Get(id);

            if (app == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update appointment request" });
            }

            app.DateTimeStart = model.DateTimeStart;

            var result = await repository.Update(id, app);

            if (result)
            {
                logger.LogInformation("Successfully updated an appointment request with the following id: " + app.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to update an appointment request" });
            }
        }

        [HttpPut("{id}/Confirm")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            string teacherId = userManager.GetUserId(HttpContext.User);

            var teacher = await personRepository.Get(teacherId);

            if (teacher == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to confirm an appointment request" });
            }

            logger.LogInformation("Attempting an confirm an appointment request with the following id: " + id);

            var app = await repository.GetInclude(id);

            if (app == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to confirm appointment request" });
            }

            var appointment = new Appointment { Id = Guid.NewGuid(), DateTimeStart = app.DateTimeStart, Duration = 90, Students = new List<Person> { app.RequestedBy}, Teacher = teacher, TeacherId = teacher.Id  };

            var result = await appointmentRepository.Create(appointment);

            if (result)
            {
                logger.LogInformation("Successfully confirmed an appointment request with the following id: " + appointment.Id);

                app.ApprovedBy = teacher;
                app.Approved = true;

                if(await repository.Update(app.Id, app))
                {
                    return Ok(new { code = 0, errorMessage = "" });
                }
                else
                {
                    return Ok(new { code = 1, errorMessage = "Approved, but unable to write by who" });
                }
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to confirm an appointment request" });
            }
        }

        [HttpPut("{id}/Deny")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Deny(Guid id)
        {
            string teacherId = userManager.GetUserId(HttpContext.User);

            var teacher = await personRepository.Get(teacherId);

            if (teacher == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to deny an appointment request" });
            }

            logger.LogInformation("Attempting an deny an appointment request with the following id: " + id);

            var app = await repository.Get(id);

            if (app == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to deny appointment request" });
            }

            var result = await repository.Delete(app.Id);

            if (result)
            {
                logger.LogInformation("Successfully denied an appointment request with the following id: " + app.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to deny an appointment request" });
            }
        }

        // DELETE api/<AppointmentRequestsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Student, Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {

            logger.LogInformation("Attempting to delete an appointment request with the following id: " + id);

            var app = await repository.Get(id);

            if (app == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to delete appointment request" });
            }

            var result = await repository.Delete(id);

            if (result)
            {
                logger.LogInformation("Successfully deleted an appointment request with the following id: " + app.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to delete appointment request" });
            }
        }
    }
}
