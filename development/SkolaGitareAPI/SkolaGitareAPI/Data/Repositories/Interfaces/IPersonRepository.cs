using SkolaGitareAPI.Data.DTOs;
using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories.Interfaces
{
    public interface IPersonRepository : IGeneralRepository<Person>
    {
        Task<List<PersonDTO>> GetStudents();

        Task<List<PersonDTO>> GetTeachers();

        Task<List<PersonDTO>> GetAdmins();

        Task<List<PersonDTO>> GetStudentRegistrationRequests();

        Task<List<PersonDTO>> GetTeacherRegistrationRequests();

        Task<List<PersonDTO>> GetAdminRegistrationRequests();

        Task<bool> ChangeMembershipType(Guid newTypeId, string studentId);
    }
}
