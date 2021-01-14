using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Models
{
    public class UpdateMembershipModel
    {
        public Guid? TypeId { get; set; }

        public string TypeName { get; set; }
    }
}
