using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; }
        [Required]
        public Person For { get; set; }
        [Required]
        public Person From { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Notes { get; set; }
        [MaxLength(100)]
        public string DownloadLink { get; set; }
        public DateTime LessonDate { get; set; }
    }
}
