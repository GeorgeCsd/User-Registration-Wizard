using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistWizard.Api.Dtos;

public class UserDto
{
    [Required, StringLength(120)]
    public string Name { get; set; } = default!;

    [Required, StringLength(120)]
    public string FirstName { get; set; } = default!;

    [Required, StringLength(80)]
    public string UserName { get; set; } = default!;

    [Required, MinLength(8), MaxLength(120)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&._\-])[A-Za-z\d@$!%*?&._\-]+$",
    ErrorMessage = " Password must contain at least one uppercase letter, one digit, and one special character "
    )]
    public string Password { get; set; } = default!;

    [Required, Compare("Password", ErrorMessage = " Passwords do not match ")]
    public string PasswordRepeat { get; set; } = default!;

    [EmailAddress, StringLength(200)]
    public string? Email { get; set; }

}



