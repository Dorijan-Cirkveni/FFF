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
    public class AppointmentRepository : GeneralRepository<Appointment>, IAppointmentRepository
    {
        private readonly IMapper mapper;
        public AppointmentRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<bool> AddStudents(Guid id, IEnumerable<string> studentIds)
        {
            if(studentIds == null)
            {
                return false;
            }

            if(id == default(Guid))
            {
                return false;
            }

            var app = await context.Appointments.FindAsync(id);

            foreach(var studentId in studentIds)
            {
                var std = await context.People.FindAsync(studentId);

                if(std == null)
                {
                    return false;
                }

                app.Students.Add(std);
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<Guid?> CreateAppointment(DateTime start, int duration, Person teacher, List<string> studentsIds)
        {
            var appointment = new Appointment();
            appointment.Id = Guid.NewGuid();
            appointment.DateTimeStart = start;
            appointment.Duration = duration;
            appointment.Students = new List<Person>();
            appointment.Teacher = teacher;

            if (teacher == null)
            {
                return null;
            }

            foreach (var id in studentsIds)
            {
                var std = await context.People.Include(x => x.Appointments).Where(x => x.Id == id).FirstOrDefaultAsync();

                if (std == null)
                {
                    return null;
                }

                appointment.Students.Add(std);
            }

            context.Add(appointment);

            await context.SaveChangesAsync();

            return appointment.Id;
        }

        public async Task<List<AppointmentDTO>> GetAppointmentsWithStudents()
        {
            return await context.Appointments.Select(x => x).ProjectTo<AppointmentDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> RemoveStudents(Guid id, IEnumerable<string> studentIds)
        {
            if (studentIds == null)
            {
                return false;
            }

            if (id == default(Guid))
            {
                return false;
            }

            var app = await context.Appointments.FindAsync(id);

            foreach (var studentId in studentIds)
            {
                var std = app.Students.Where(x => x.Id == studentId).FirstOrDefault();

                if (std == null)
                {
                    return false;
                }

                app.Students.Remove(std);
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
