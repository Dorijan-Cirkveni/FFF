using SkolaGitareAPI.Data.DTOs;
using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories.Interfaces
{
    public interface IMembershipRepository : IGeneralRepository<Membership>
    {
        public Task<List<MembershipDTO>> GetMemberships();

        public Task<MembershipDTO> GetMembership(Guid id);
    }
}
