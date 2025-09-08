using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistWizard.Api;
using RegistWizard.Api.Dtos;
using RegistWizard.Api.Models;

namespace RegistWizard.Api.Controllers
{
    /// <summary>
    /// API controller for handling user registration.
    /// </summary>
    /// <remarks>
    /// Exposes endpoints to register new companies and users.  
    /// Current functionality:  
    /// - POST /api/registration → creates a company and a user account in one transaction.  
    /// </remarks>
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly AppDbContext appDbContext;
        private readonly UserManager<AppUser> userManager;

        public RegistrationController(AppDbContext dbContext, UserManager<AppUser> manager)
        {

            appDbContext = dbContext;
            userManager = manager;

        }

        /// <summary>
        /// Registers a new company and a user account in the system.
        /// </summary>
        /// <remarks>
        /// Input: A <see cref="RegistrationRequest"/> containing company and user details.  
        /// Output: A <see cref="RegistrationResponse"/> indicating success or failure with a message.  
        /// </remarks>
        /// <param name="request">The registration data (company info and user credentials).</param>
        /// <returns>
        /// 200 OK → registration succeeded.  
        /// 400 Bad Request → invalid data or errors during creation.  
        /// 409 Conflict → username already exists.  
        /// 
        [HttpPost]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
        {

            var industry = await appDbContext.Industries.AnyAsync(i => i.Id == request.Company.IndustryId);

            if (!industry)
                return BadRequest(new RegistrationResponse(false, "Invalid industry"));

            var usernameTaken = await userManager.FindByNameAsync(request.User.UserName.Trim()) is not null;
            if (usernameTaken)
                return Conflict(new RegistrationResponse(false, "This Username is already taken"));

            await using var transaction = await appDbContext.Database.BeginTransactionAsync();
            try
            {
                var company = new Company
                {
                    Name = request.Company.Name.Trim(),
                    IndustryId = request.Company.IndustryId

                };
                appDbContext.Companies.Add(company);
                await appDbContext.SaveChangesAsync();

                var user = new AppUser
                {
                    UserName = request.User.UserName.Trim(),
                    Email = string.IsNullOrWhiteSpace(request.User.Email) ? null : request.User.Email!.Trim(),
                    FirstName = request.User.FirstName.Trim(),
                    Name = request.User.Name.Trim(),
                    CompanyId = company.Id

                };

                var result = await userManager.CreateAsync(user, request.User.Password);

                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();
                    var message = string.Join("; ", result.Errors.Select(e => e.Description));
                    return BadRequest(new RegistrationResponse(false, message));
                }
                await transaction.CommitAsync();
                return Ok(new RegistrationResponse(true, " Registration is completed "));

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}