using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class AppointmentModel
    {
        public Guid Id { get; set; }
        public DateTime DateTimeStart { get; set; }
        public int Duration { get; set; }
        public PersonModel Teacher { get; set; }
        public List<PersonModel> Students { get; set; }

    }
}
