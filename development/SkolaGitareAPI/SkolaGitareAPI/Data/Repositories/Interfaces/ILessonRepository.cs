using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories.Interfaces
{
    public interface ILessonRepository : IGeneralRepository<Lesson>
    {
        public Task<IEnumerable<Lesson>> GetStudentLessons(object studentId);

        Task<List<Lesson>> GetEarliestLessonForStudentsInNextAppointment();
    }
}
