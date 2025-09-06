using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistWizard.Api.Models;

namespace RegistWizard.Api.Controllers
{


    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> manager)
        {
            userManager = manager;
        }

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