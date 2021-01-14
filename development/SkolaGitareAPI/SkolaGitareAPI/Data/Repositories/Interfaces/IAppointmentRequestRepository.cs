using SkolaGitareAPI.Data.DTOs;
using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories.Interfaces
{
    public interface IAppointmentRequestRepository : IGeneralRepository<AppointmentRequest>
    {
        public Task<AppointmentRequest> GetInclude(Guid id);

        public Task<List<AppointmentRequestDTO>> GetAppointmentRequestsWithUserInfo();

        public Task<AppointmentRequestDTO> GetAppointmentRequestWithUserInfo(Guid id);
    }
}
