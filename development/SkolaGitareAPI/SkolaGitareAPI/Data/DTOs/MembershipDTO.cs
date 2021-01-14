using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.DTOs
{
    public class MembershipDTO
    {
        public Guid Id { get; set; }
        public PersonDTO Member { get; set; }
        public MembershipTypeDTO Type { get; set; }
    }
}
