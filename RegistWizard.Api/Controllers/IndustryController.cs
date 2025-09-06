using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistWizard.Api;           
using System.Linq;

namespace RegistWizard.Api.Controllers
{
    
    [Route("api/industry")]
    [ApiController]
public class IndustryController : ControllerBase
{
        private readonly AppDbContext appDbContext;
        public IndustryController(AppDbContext dbContext)
        {
            appDbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await appDbContext.Industries
            .OrderBy( i => i.Name )
            .Select( i => new {i.Id, i.Name} )
            .ToListAsync();

        return Ok( items );
    }
}
}