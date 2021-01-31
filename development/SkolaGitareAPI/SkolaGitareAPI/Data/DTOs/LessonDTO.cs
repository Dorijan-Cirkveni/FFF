using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.DTOs
{
    public class LessonDTO
    {
        public Guid Id { get; set; }
        public PersonDTO For { get; set; }
        public PersonDTO From { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string DownloadLink { get; set; }
        public DateTime LessonDate { get; set; }
    }
}
