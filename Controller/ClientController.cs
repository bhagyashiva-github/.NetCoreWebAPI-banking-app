using coreAPI_banking_app.Models;
using coreAPIData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coreAPIData.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")] // Swagger grouping
    public class ClientController : ControllerBase
    {
        private readonly PostgresContext _context;

        public ClientController(PostgresContext context)
        {
            _context = context;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetClients(int page = 1, int pageSize = 10, string sortBy = "Name", bool descending = false)
        {
            var query = _context.Clients.AsQueryable();

            // Apply Sorting
            query = sortBy switch
            {
                "Name" => descending ? query.OrderByDescending(c => c.Fullname) : query.OrderBy(c => c.Fullname),
                "Email" => descending ? query.OrderByDescending(c => c.Email) : query.OrderBy(c => c.Email),
                _ => query.OrderBy(c => c.Clientid)
            };

            // Apply Pagination
            var totalItems = await query.CountAsync();
            var clients = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Items = clients
            };

            return Ok(response);
        }
    }
}


