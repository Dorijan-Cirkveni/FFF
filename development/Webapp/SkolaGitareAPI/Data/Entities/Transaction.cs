using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Entities
{
    public class Transaction
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Membership Membership { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public bool Paid { get; set; }

    }
}
