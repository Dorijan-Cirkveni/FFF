using Microsoft.EntityFrameworkCore;
using SkolaGitareAPI.Data.Entities;
using SkolaGitareAPI.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories
{
    public class MembershipTypeRepository : GeneralRepository<MembershipType>, IMembershipTypeRepository
    {
        public MembershipTypeRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<MembershipType> GetByName(string name)
        {
            return await context.MembershipTypes.Where(x => x.Name.Equals(name)).FirstOrDefaultAsync();
        }
    }
}
