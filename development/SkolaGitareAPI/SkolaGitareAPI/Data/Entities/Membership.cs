using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Entities
{
    public class Membership
    {
        public Guid Id { get; set; }
        public Person Member { get; set; }

        public MembershipType Type { get; set; }
    }
}
