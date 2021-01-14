using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Models
{
    public class UpdateAppointmentTimeModel
    {
        [Required]
        public Guid AppointmentId { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}
