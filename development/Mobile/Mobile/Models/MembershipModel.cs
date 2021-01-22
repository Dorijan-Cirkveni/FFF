using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class MembershipModel
    {
        public Guid Id { get; set; }
        public PersonModel Member { get; set; }
        public MembershipTypeModel Type { get; set; }
    }
}
