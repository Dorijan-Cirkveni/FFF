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
    [Authorize(Roles = "Admin, Teacher")]
    [ApiController]
    public class MembershipsController : ControllerBase
    {
        private readonly IMembershipRepository repository;
        private readonly IPersonRepository personRepository;
        private readonly IMembershipTypeRepository membershipTypeRepository;
        private readonly UserManager<Person> userManager;
        private readonly ILogger<MembershipsController> logger;

        public MembershipsController(IMembershipRepository repository, IPersonRepository personRepository, IMembershipTypeRepository membershipTypeRepository, UserManager<Person> userManager, ILogger<MembershipsController> logger)
        {
            this.repository = repository;
            this.personRepository = personRepository;
            this.membershipTypeRepository = membershipTypeRepository;
            this.userManager = userManager;
            this.logger = logger;
        }

        // GET: api/<MembershipsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // GET api/<MembershipsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            var membership = await repository.Get(id);

            if (membership == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(membership);
            }
        }

        // POST api/<MembershipsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMembershipModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create membership" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create membership" });
            }

            var member = await personRepository.Get(model.Member);

            MembershipType type;

            if(model.TypeId != null)
            {
                type = await membershipTypeRepository.Get(model.TypeId);
            }
            else
            {
                type = await membershipTypeRepository.GetByName(model.TypeName);
            }

            if (type == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create membership" });
            }

            logger.LogInformation("Attempting to create membership");

            Membership membership = new Membership
            {
                Id = Guid.NewGuid(),
                Member = member,
                Type = type
            };

            var result = await repository.Create(membership);

            if (!result)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create membership" });
            }

            return Ok(membership.Id);
        }

        // PUT api/<MembershipsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateMembershipModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update membership" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update membership" });
            }

            MembershipType type;

            if (model.TypeId != null)
            {
                type = await membershipTypeRepository.Get(model.TypeId);
            }
            else
            {
                type = await membershipTypeRepository.GetByName(model.TypeName);
            }

            if(type == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update membership" });
            }

            logger.LogInformation("Attempting to update membership with the following id: " + id);

            var membership = await repository.Get(id);

            if (membership == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update membership" });
            }

            membership.Type = type;

            var result = await repository.Update(id, membership);

            if (result)
            {
                logger.LogInformation("Successfully updated membership with the following id: " + membership.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to update membership" });
            }
        }

        // DELETE api/<MembershipsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation("Attempting to delete membership with the following id: " + id);

            var membership = await repository.Get(id);

            if (membership == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to delete membership" });
            }

            var result = await repository.Delete(id);

            if (result)
            {
                logger.LogInformation("Successfully deleted membership with the following id: " + membership.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to delete membership" });
            }
        }
    }
}
