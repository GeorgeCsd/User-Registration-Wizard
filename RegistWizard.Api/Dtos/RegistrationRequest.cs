using System.ComponentModel.DataAnnotations;

namespace RegistWizard.Api.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a registration request.
    /// </summary>
    /// <remarks>
    /// Contains all required information to register a new company and user account.  
    /// Includes company details, user details, and a summary step.  
    /// </remarks>
    public class RegistrationRequest
    {
        [Required]
        public CompanyDto Company { get; set; } = new();

        [Required]
        public SummaryStepDto Summary { get; set; } = new();

        [Required]
        public UserDto User { get; set; } = new();

    }
}