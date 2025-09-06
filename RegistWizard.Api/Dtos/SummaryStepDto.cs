using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace RegistWizard.Api.Dtos
{
    public class SummaryStepDto
    {
        [Range(typeof(bool), "true", "true", ErrorMessage = " You must accept the Terms of Service ")]
        public bool TermsofServiceAccepted { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = " You must accept the Privacy Policy ")]
        public bool PrivacyPolicyAccepted { get; set; }

    }

}