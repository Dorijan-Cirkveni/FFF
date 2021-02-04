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
    [Authorize(Roles = "Admin, Teacher")]
    [ApiController]
    public class MembershipTypesController : ControllerBase
    {
        private readonly IMembershipTypeRepository repository;
        private readonly IPersonRepository personRepository;
        private readonly IMembershipTypeRepository membershipTypeRepository;
        private readonly UserManager<Person> userManager;
        private readonly ILogger<MembershipTypesController> logger;

        public MembershipTypesController(IMembershipTypeRepository repository, IPersonRepository personRepository, IMembershipTypeRepository membershipTypeRepository, UserManager<Person> userManager, ILogger<MembershipTypesController> logger)
        {
            this.repository = repository;
            this.personRepository = personRepository;
            this.membershipTypeRepository = membershipTypeRepository;
            this.userManager = userManager;
            this.logger = logger;
        }


        // GET: api/<MembershipTypesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // GET api/<MembershipTypesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            var membershipType = await repository.Get(id);

            if (membershipType == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(membershipType);
            }
        }

        // POST api/<MembershipTypesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMembershipTypeModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create membership type" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create membership type" });
            }

            logger.LogInformation("Attempting to create membership type");

            MembershipType membership = new MembershipType
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price
            };

            var result = await repository.Create(membership);

            if (!result)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create membership type" });
            }

            return Ok(membership.Id);
        }

        // PUT api/<MembershipTypesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateMembershipTypeModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update membership type" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update membership type" });
            }

            var type = await membershipTypeRepository.Get(id);

            if (type == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update membership type" });
            }

            logger.LogInformation("Attempting to update membership type with the following id: " + id);

            var result = await repository.Update(id, type);

            if (result)
            {
                logger.LogInformation("Successfully updated membership type with the following id: " + type.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to update membership type" });
            }
        }
    }
}
