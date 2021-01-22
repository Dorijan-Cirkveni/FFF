using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public MembershipModel Membership { get; set; }
        public DateTime Date { get; set; }
        public bool Paid { get; set; }
    }
}
