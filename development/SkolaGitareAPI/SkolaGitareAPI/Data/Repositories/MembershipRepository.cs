using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SkolaGitareAPI.Data.DTOs;
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
        private readonly IMapper mapper;
        public MembershipRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<MembershipDTO> GetMembership(Guid id)
        {
            return await context.Memberships.Include(x => x.Member).Include(x => x.Type).Where(x => x.Id == id).ProjectTo<MembershipDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<List<MembershipDTO>> GetMemberships()
        {
            return await context.Memberships.Include(x => x.Member).Include(x => x.Type).ProjectTo<MembershipDTO>(mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
