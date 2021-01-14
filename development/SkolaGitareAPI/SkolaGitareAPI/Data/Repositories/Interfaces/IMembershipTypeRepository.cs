using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories.Interfaces
{
    public interface IMembershipTypeRepository : IGeneralRepository<MembershipType>
    {
        public Task<MembershipType> GetByName(string name);
    }
}
