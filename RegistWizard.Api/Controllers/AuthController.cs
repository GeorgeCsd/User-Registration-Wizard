using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistWizard.Api.Models;
using RegistWizard.Api.Dtos;

namespace RegistWizard.Api.Controllers
{
    /// <summary>
    /// API controller for authentication operations.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for user login and retrieving the current session user.  
    /// Current functionality:  
    /// - POST /api/authentication/login → authenticates a user with username & password.  
    /// - GET /api/authentication/me → retrieves basic info about the currently logged-in user (demo).  
    /// </remarks>
    [ApiController]
    [Route("api/authentication")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(UserManager<AppUser> manager, SignInManager<AppUser> signManager)
        {
            userManager = manager;
            signInManager = signManager;
        }

        /// <summary>
        /// Authenticates a user with username and password.
        /// </summary>
        /// <remarks>
        /// Input: JSON body containing <c>UserName</c> and <c>Password</c>.  
        /// Output:  
        /// - 200 OK → returns a <see cref="LoginResponse"/> object with user details if authentication succeeds.  
        /// - 401 Unauthorized → if the username or password is invalid.  
        /// </remarks>
        /// <param name="request">The login request containing username and password.</param>
        /// <returns>
        /// A JSON response indicating success or failure of the login attempt.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName.Trim());
            if (user == null)
                return Unauthorized(new LoginResponse { Success = false, Message = "Invalid username" });

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
                return Unauthorized(new LoginResponse { Success = false, Message = "Invalid password" });

            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Login completed",
                UserName = user.UserName,
                Name = user.Name,
                FirstName = user.FirstName,
                CompanyId = user.CompanyId
            });
        }

    }
}