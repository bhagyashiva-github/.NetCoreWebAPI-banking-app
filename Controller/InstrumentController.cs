using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coreAPI_banking_app.Models;
using coreAPIData;

namespace coreAPIData.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]// Swagger grouping

    public class InstrumentController : ControllerBase
    {
        private readonly PostgresContext _context;

        public InstrumentController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInstruments(
            int page = 1,
            int pageSize = 10,
            string sortBy = "Name",
            bool descending = false,
            string? typeFilter = null)
        {
            var query = _context.Instruments.AsQueryable();

            // Optional filter by Type
            if (!string.IsNullOrEmpty(typeFilter))
            {
                query = query.Where(i => i.Type == typeFilter);
            }

            // Dynamic sorting
            query = sortBy switch
            {
                "Market" => descending ? query.OrderByDescending(i => i.Market) : query.OrderBy(i => i.Market),
                "Currency" => descending ? query.OrderByDescending(i => i.Currency) : query.OrderBy(i => i.Currency),
                _ => descending ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name),
            };

            // Pagination
            var totalItems = await query.CountAsync();
            var instruments = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!instruments.Any()) return NotFound("No instruments found.");

            var result = new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Items = instruments
            };

            return Ok(result);
        }
    }
}
