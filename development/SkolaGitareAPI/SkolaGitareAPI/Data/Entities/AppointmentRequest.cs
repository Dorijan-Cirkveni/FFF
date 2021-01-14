using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Entities
{
    public class AppointmentRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime DateTimeStart { get; set; }
        [Required]
        public Person RequestedBy { get; set; }


        public bool Approved { get; set; }

        public Person ApprovedBy { get; set; }
    }
}
