using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace RegistWizard.Api.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) representing the summary step of registration.
    /// </summary>
    /// <remarks>
    /// Contains user confirmations required to complete the registration process,  
    /// such as accepting Terms of Service and Privacy Policy.  
    /// </remarks>
    public class SummaryStepDto
    {
        [Range(typeof(bool), "true", "true", ErrorMessage = " You must accept the Terms of Service ")]
        public bool TermsofServiceAccepted { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = " You must accept the Privacy Policy ")]
        public bool PrivacyPolicyAccepted { get; set; }

    }

}