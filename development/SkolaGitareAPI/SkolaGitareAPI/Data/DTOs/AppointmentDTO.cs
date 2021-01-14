using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.DTOs
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public DateTime DateTimeStart { get; set; }
        public int Duration { get; set; }
        public PersonDTO Teacher { get; set; }
        public List<PersonDTO> Students { get; set; }
    }
}
