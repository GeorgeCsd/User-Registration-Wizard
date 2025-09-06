using System.ComponentModel.DataAnnotations;

namespace RegistWizard.Api.Dtos
{
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