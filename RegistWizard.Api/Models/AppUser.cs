using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RegistWizard.Api.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public int CompanyId { get; set; } 
        public Company Company { get; set; } = default!;
    }
}