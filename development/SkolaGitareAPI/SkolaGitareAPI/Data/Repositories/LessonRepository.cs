using Microsoft.EntityFrameworkCore;
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
        public LessonRepository(ApplicationDbContext context) : base(context)
        {

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
    }
}
