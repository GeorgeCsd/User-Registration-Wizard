using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistWizard.Api.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) representing the result of a login attempt.
    /// </summary>
    /// <remarks>
    /// Returned by the authentication endpoint after a user submits valid or invalid login credentials.  
    /// Provides information about whether the login was successful, an explanatory message,  
    /// and optionally additional user details or a token (if token-based authentication is enabled).
    /// </remarks>
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
        public DateTime? ExpiresAtUtc { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public int? CompanyId { get; set; }

    }
}