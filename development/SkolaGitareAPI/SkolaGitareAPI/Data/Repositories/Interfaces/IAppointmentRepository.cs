using SkolaGitareAPI.Data.DTOs;
using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories.Interfaces
{
    public interface IAppointmentRepository : IGeneralRepository<Appointment>
    {
        public Task<Guid?> CreateAppointment(DateTime start, int duration, Person teacher, List<string> studentsIds);

        public Task<bool> AddStudents(Guid id, IEnumerable<string> studentIds);

        public Task<bool> RemoveStudents(Guid id, IEnumerable<string> studentIds);

        public Task<List<AppointmentDTO>> GetAppointmentsWithStudents();
    }
}
