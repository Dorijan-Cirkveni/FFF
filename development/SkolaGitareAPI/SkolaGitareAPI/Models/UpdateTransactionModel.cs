using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Models
{
    public class UpdateTransactionModel
    {
        [Required]
        public Guid MembershipId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public bool Paid { get; set; }
    }
}
