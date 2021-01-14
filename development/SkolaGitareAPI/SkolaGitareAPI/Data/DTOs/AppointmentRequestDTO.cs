using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.DTOs
{
    public class AppointmentRequestDTO
    {
        public Guid Id { get; set; }
        public DateTime DateTimeStart { get; set; }
        public PersonDTO RequestedBy { get; set; }
        public bool Approved { get; set; }
        public PersonDTO ApprovedBy { get; set; }
    }
}
