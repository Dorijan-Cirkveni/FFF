using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Models
{
    public class CreateAppointmentModel
    {
        [Required]
        public DateTime DateTimeStart { get; set; }
        [Required]
        public int Duration { get; set; }

        public IEnumerable<string> StudentsIds { get; set; }
    }
}
