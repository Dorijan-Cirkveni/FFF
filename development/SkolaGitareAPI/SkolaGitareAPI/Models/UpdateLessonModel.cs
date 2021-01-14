using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Models
{
    public class UpdateLessonModel
    {
        public string Name { get; set; }
        [MaxLength(500)]
        public string Notes { get; set; }
        [MaxLength(100)]
        public string DownloadLink { get; set; }
    }
}
