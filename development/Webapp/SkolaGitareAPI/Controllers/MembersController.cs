using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkolaGitareAPI.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Teacher, Admin")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IPersonRepository repository;

        public MembersController(IPersonRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("Students")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await repository.GetStudents());
        }

        [HttpGet("Teachers")]
        public async Task<IActionResult> GetTeachers()
        {
            return Ok(await repository.GetTeachers());
        }

        [HttpGet("Admins")]
        public async Task<IActionResult> GetAdmins()
        {
            return Ok(await repository.GetAdmins());
        }

        [HttpGet("Students/Requests")]
        public async Task<IActionResult> GetStudentRegistrationRequests()
        {
            return Ok(await repository.GetStudentRegistrationRequests());
        }

        [HttpGet("Teachers/Requests")]
        public async Task<IActionResult> GetTeacherRegistrationRequests()
        {
            return Ok(await repository.GetTeacherRegistrationRequests());
        }

        [HttpGet("Admins/Requests")]
        public async Task<IActionResult> GetAdminRegistrationRequests()
        {
            return Ok(await repository.GetAdminRegistrationRequests());
        }


        [HttpPut("Student/{studentId}/ChangeMembershipType/{newTypeId}")]
        public async Task<IActionResult> ChangeMembershipType(string studentId, Guid newTypeId)
        {
            var result = await repository.ChangeMembershipType(newTypeId, studentId);

            if(result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
