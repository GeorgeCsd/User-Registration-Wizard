using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistWizard.Api;
using System.Linq;

namespace RegistWizard.Api.Controllers
{

    /// <summary>
    /// API controller for managing industries.
    /// </summary>
    /// <remarks>
    /// Exposes endpoints to fetch industry data from the database.  
    /// Current functionality:  
    /// - GET /api/industry → returns a list of industries (Id and Name).  
    /// </remarks>
    [Route("api/industry")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public IndustryController(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        /// <summary>
        /// Retrieves all industries from the database, ordered by name.
        /// </summary>
        /// <remarks>
        /// Input: No parameters are required in the request.  
        /// Output: A list of industries (Id and Name) in JSON format.  
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await appDbContext.Industries
                .OrderBy(i => i.Name)
                .Select(i => new { i.Id, i.Name })
                .ToListAsync();

            return Ok(items);
        }
    }
}