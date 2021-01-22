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
    [Authorize(Roles = "Teacher, Admin")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository repository;
        private readonly IPersonRepository personRepository;
        private readonly IMembershipRepository membershipRepository;
        private readonly UserManager<Person> userManager;
        private readonly ILogger<TransactionsController> logger;

        public TransactionsController(  ITransactionRepository repository, 
                                        IPersonRepository personRepository, 
                                        IMembershipRepository membershipRepository, 
                                        UserManager<Person> userManager, 
                                        ILogger<TransactionsController> logger)
        {
            this.repository = repository;
            this.personRepository = personRepository;
            this.membershipRepository = membershipRepository;
            this.userManager = userManager;
            this.logger = logger;
        }




        // GET: api/<TransactionsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetAllTransactions());
        }

        [HttpGet("Unapid")]
        public async Task<IActionResult> GetUnpaidTransactions()
        {
            return Ok(await repository.GetUnpaidTransactions());
        }

        [HttpGet("Student/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var transactions = await repository.GetTransactionsByStudent(id);

            if (transactions == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(transactions);
            }
        }

        [HttpGet("Student/{id}/Transaction")]
        public async Task<IActionResult> Get(string id, [FromQuery] DateTime date)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var transaction = await repository.GetTransactionByStudentAndDate(id, date);

            if (transaction == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(transaction);
            }
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            var transaction = await repository.GetTransaction(id);

            if (transaction == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(transaction);
            }
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTransactionModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create transaction" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create transaction" });
            }

            var membership = await membershipRepository.Get(model.MembershipId);

            if (membership == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create transaction" });
            }

            logger.LogInformation("Attempting to create transaction");

            Transaction transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Membership = membership,
                Date = model.Date,
                Paid = model.Paid
            };

            var result = await repository.Create(transaction);

            if (!result)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to create transaction" });
            }

            return Ok(transaction.Id);
        }

        // PUT api/<TransactionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateTransactionModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update transaction" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update transaction" });
            }

            var membership = await membershipRepository.Get(model.MembershipId);

            if (membership == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to update transaction" });
            }

            var transaction = await repository.Get(id);

            if (transaction == null)
            {
                return BadRequest(new { code = 1, errorMessage = "Unable to update transaction" });
            }

            transaction.Date = model.Date;
            transaction.Paid = model.Paid;
            transaction.Membership = membership;

            var result = await repository.Update(id, transaction);

            if (result)
            {
                logger.LogInformation("Successfully updated transaction with the following id: " + transaction.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to update transaction" });
            }
        }

        [HttpPut("{id}/Paid")]
        public async Task<IActionResult> Paid(Guid id)
        {

            var transaction = await repository.Get(id);

            if (transaction == null)
            {
                return BadRequest(new { code = 1, errorMessage = "Unable to update transaction" });
            }

            transaction.Paid = true;

            var result = await repository.Update(id, transaction);

            if (result)
            {
                logger.LogInformation("Successfully updated transaction with the following id: " + transaction.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to update transaction" });
            }
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation("Attempting to delete transaction with the following id: " + id);

            var transaction = await repository.Get(id);

            if (transaction == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Unable to delete transaction" });
            }

            var result = await repository.Delete(id);

            if (result)
            {
                logger.LogInformation("Successfully deleted transaction with the following id: " + transaction.Id);
                return Ok(new { code = 0, errorMessage = "" });
            }

            else
            {
                logger.LogInformation("FAILED");
                return BadRequest(new { code = -1, errorMessage = "Unable to delete transaction" });
            }
        }
    }
}
