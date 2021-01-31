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
    public class LessonRepository : GeneralRepository<Lesson>, ILessonRepository
    {
        private readonly IMapper mapper;
        public LessonRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Lesson>> GetStudentLessons(object studentId)
        {
            return await context.Lessons.Where(x => x.For.Id.Equals(studentId)).ToListAsync();
        }

        public async Task<List<Lesson>> GetEarliestLessonForStudentsInNextAppointment()
        {
            List<Lesson> lessons = new List<Lesson>();
            var appointment = await context.Appointments.Include(x => x.Students).Where(x => x.DateTimeStart >= DateTime.Now).AsNoTracking().FirstOrDefaultAsync();
            if (appointment == null)
            {
                return lessons;
            }

            foreach (var student in appointment.Students)
            {
                var lesson = await context.Lessons.Where(x => x.For.Id.Equals(student.Id)).OrderByDescending(x => x.LessonDate).FirstOrDefaultAsync();
                if (lesson != null)
                {
                    lessons.Add(lesson);
                }
            }
            return lessons;
        }

        public async Task<List<LessonDTO>> GetLessons()
        {
            return await context.Lessons.Include(x => x.For).Include(x => x.From).ProjectTo<LessonDTO>(mapper.ConfigurationProvider).
                AsNoTracking().
                ToListAsync();
        }

        public async Task<LessonDTO> GetLesson(Guid id)
        {
            return await context.Lessons.
                Include(x => x.For).
                Include(x => x.From).
                Where(x => x.Id == id).
                ProjectTo<LessonDTO>(mapper.ConfigurationProvider).
                AsNoTracking().
                FirstOrDefaultAsync();
        }
    }
}
