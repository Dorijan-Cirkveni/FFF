using SkolaGitareAPI.Data.DTOs;
using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories.Interfaces
{
    public interface ILessonRepository : IGeneralRepository<Lesson>
    {
        public Task<List<LessonDTO>> GetLessons();

        public Task<LessonDTO> GetLesson(Guid id);
        public Task<IEnumerable<Lesson>> GetStudentLessons(object studentId);

        Task<List<Lesson>> GetEarliestLessonForStudentsInNextAppointment();
    }
}
