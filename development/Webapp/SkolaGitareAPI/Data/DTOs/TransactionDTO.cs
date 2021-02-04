using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.DTOs
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public MembershipDTO Membership { get; set; }
        public DateTime Date { get; set; }
        public bool Paid { get; set; }
    }
}
