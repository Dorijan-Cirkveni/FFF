using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class PersonRepository : GeneralRepository<Person>, IPersonRepository
    {
        private readonly UserManager<Person> userManager;
        private readonly IMapper mapper;
        public PersonRepository(ApplicationDbContext context, UserManager<Person> userManager, IMapper mapper) : base(context)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<bool> ChangeMembershipType(Guid newTypeId, string studentId)
        {
            var membership = await context.Memberships.Include(x => x.Member).Where(x => x.Member.Id == studentId).FirstOrDefaultAsync();

            if(membership == null)
            {
                return false;
            }

            var type = await context.MembershipTypes.FindAsync(newTypeId);

            if(type == null)
            {
                return false;
            }

            membership.Type = type;

            try
            {
                context.Update(membership);

                await context.SaveChangesAsync();

                return true;

            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<List<PersonDTO>> GetAdminRegistrationRequests()
        {
            return mapper.Map<List<Person>, List<PersonDTO>>((await userManager.GetUsersInRoleAsync("Admin")).Where(x => x.Verified == false).ToList());
        }

        public async Task<List<PersonDTO>> GetAdmins()
        {
            return mapper.Map<List<Person>, List<PersonDTO>>((await userManager.GetUsersInRoleAsync("Admin")).Where(x => x.Verified == true).ToList());
        }

        public async Task<List<PersonDTO>> GetStudentRegistrationRequests()
        {
            return mapper.Map<List<Person>, List<PersonDTO>>((await userManager.GetUsersInRoleAsync("Student")).Where(x => x.Verified == false).ToList());
        }

        public async Task<List<PersonDTO>> GetStudents()
        {
            return mapper.Map<List<Person>, List<PersonDTO>>((await userManager.GetUsersInRoleAsync("Student")).Where(x => x.Verified == true).ToList());
        }

        public async Task<List<PersonDTO>> GetTeacherRegistrationRequests()
        {
            return mapper.Map<List<Person>, List<PersonDTO>>((await userManager.GetUsersInRoleAsync("Teacher")).Where(x => x.Verified == true).ToList());
        }

        public async Task<List<PersonDTO>> GetTeachers()
        {
            return mapper.Map<List<Person>, List<PersonDTO>>((await userManager.GetUsersInRoleAsync("Teacher")).Where(x => x.Verified == true).ToList());
        }
    }
}
