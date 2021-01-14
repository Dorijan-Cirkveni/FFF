using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Models
{
    public class UpdateMembershipTypeModel
    {
        [Required]
        public decimal Price { get; set; }
    }
}
