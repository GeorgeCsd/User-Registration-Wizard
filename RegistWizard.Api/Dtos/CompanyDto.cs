using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistWizard.Api.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a company for registration requests.
    /// </summary>
    /// <remarks>
    /// Used when creating a new company during the registration process.  
    /// Contains the company name and the industry it belongs to.  
    /// </remarks>
    public class CompanyDto
    {
        [Required, StringLength(200, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
        [Required, Range(1, int.MaxValue, ErrorMessage = " IndustryId must be a positive number ")]
        public int IndustryId { get; set; }

    }
}