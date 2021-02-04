using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Entities
{
    public class Appointment : IEquatable<Appointment>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime DateTimeStart { get; set; }
        [Required]
        public int Duration { get; set; }

        public Person Teacher { get; set; }

        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }

        [InverseProperty("Appointments")]
        public ICollection<Person> Students { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Appointment);
        }

        public bool Equals([AllowNull] Appointment other)
        {
            return other != null &&
                   Id == other.Id &&
                   DateTimeStart == other.DateTimeStart &&
                   Duration == other.Duration;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DateTimeStart, Duration);
        }

        public static bool operator ==(Appointment left, Appointment right)
        {
            return EqualityComparer<Appointment>.Default.Equals(left, right);
        }

        public static bool operator !=(Appointment left, Appointment right)
        {
            return !(left == right);
        }
    }
}
