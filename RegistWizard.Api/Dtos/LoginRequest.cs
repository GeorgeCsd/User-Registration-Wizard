using System.ComponentModel.DataAnnotations;

namespace RegistWizard.Api.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a login request.
    /// </summary>
    /// <remarks>
    /// Used when a user attempts to sign in to the system.  
    /// Contains the credentials that will be validated against the stored user account.
    /// </remarks>
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

    }
}