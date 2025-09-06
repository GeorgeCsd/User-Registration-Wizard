using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistWizard.Api.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int IndustryId { get; set; }
        public Industry Industry { get; set; } = default!;
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    }
}