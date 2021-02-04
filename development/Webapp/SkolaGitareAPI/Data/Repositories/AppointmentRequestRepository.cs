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
    public class AppointmentRequestRepository : GeneralRepository<AppointmentRequest>, IAppointmentRequestRepository
    {
        private readonly IMapper mapper;
        public AppointmentRequestRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<List<AppointmentRequestDTO>> GetAppointmentRequestsWithUserInfo()
        {
            return await context.AppointmentRequests.
                Include(x => x.RequestedBy).
                Include(x => x.ApprovedBy).
                ProjectTo<AppointmentRequestDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AppointmentRequestDTO> GetAppointmentRequestWithUserInfo(Guid id)
        {
            return await context.AppointmentRequests.
                Include(x => x.RequestedBy).
                Include(x => x.ApprovedBy).
                Where(x => x.Id == id).
                ProjectTo<AppointmentRequestDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<AppointmentRequest> GetInclude(Guid id)
        {
            return await context.AppointmentRequests.Include(x => x.RequestedBy).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
