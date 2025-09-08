using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistWizard.Api.Models;

namespace RegistWizard.Api.Controllers
{

    /// <summary>
    /// API controller for user-related operations.
    /// </summary>
    /// <remarks>
    /// Exposes endpoints to manage and validate user data.  
    /// Current functionality:  
    /// - GET /api/user/check-username → checks if a username is available.  
    /// </remarks>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> manager)
        {
            userManager = manager;
        }

        /// <summary>
        /// Checks whether a username is available for registration.
        /// </summary>
        /// <remarks>
        /// Input: Query parameter `username` (string).  
        /// Output: Boolean (true if available, false if taken).  
        /// </remarks>
        /// <param name="username">The username to validate.</param>
        /// <returns>
        /// 200 OK → returns true if available, false if taken.  
        /// 400 Bad Request → if the username is missing or invalid.  
        /// </returns>
        [HttpGet("check-username")]
        public async Task<IActionResult> CheckUsername([FromQuery] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest(false);

            var user = await userManager.FindByNameAsync(username.Trim());
            return Ok(user is null);
        }
    }
}