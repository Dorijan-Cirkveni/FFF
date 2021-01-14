using SkolaGitareAPI.Data.Entities;
using SkolaGitareAPI.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories
{
    public class MembershipRepository : GeneralRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
