using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Entities
{
    public class Person : IdentityUser, IEquatable<Person>
    {
        [MaxLength(75)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Surname { get; set; }
        [Required]
        public bool Verified { get; set; }

        [InverseProperty("Students")]
        public ICollection<Appointment> Appointments { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Person);
        }

        public bool Equals(Person other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(Person left, Person right)
        {
            return EqualityComparer<Person>.Default.Equals(left, right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !(left == right);
        }
    }
}
