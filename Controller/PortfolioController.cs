using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coreAPI_banking_app.Models;
using coreAPIData;
using Microsoft.AspNetCore.Authorization;

namespace coreAPIData.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]// Swagger grouping
    public class PortfolioController : ControllerBase
    {
        private readonly PostgresContext _context;

        public PortfolioController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPortfolios()
        {
            var portfolios = await _context.Portfolios
                .Where(p => p.Averagecost > 500)
                .OrderByDescending(p => p.Averagecost)
                .ToListAsync();

            if (!portfolios.Any()) return NotFound("No portfolios found.");

            return Ok(portfolios);
        }
    }
}
