namespace RegistWizard.Api.Dtos
{
    /// <summary>
    /// Represents the response returned after a registration attempt.
    /// </summary>
    /// <remarks>
    /// Indicates whether the registration succeeded or failed,  
    /// along with an explanatory message.  
    /// </remarks>
    /// <param name="Success">True if the registration was successful, otherwise false.</param>
    /// <param name="Message">A descriptive message about the result of the registration.</param>
    public record RegistrationResponse(bool Success, string Message);

}
