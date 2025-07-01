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
    [ApiExplorerSettings(GroupName = "v1")]

    public class TradeController : ControllerBase
    {
        private readonly PostgresContext _context;

        public TradeController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTrades(
            int page = 1,
            int pageSize = 10,
            string sortBy = "Tradedate",
            bool descending = false,
            string? tradeType = null,
            string? statusFilter = null,
            string? brokerSearch = null)
        {
            var query = _context.Trades
                .Include(t => t.Client)
                .Include(t => t.Instrument)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(tradeType))
                query = query.Where(t => t.Tradetype == tradeType);

            if (!string.IsNullOrEmpty(statusFilter))
                query = query.Where(t => t.Tradestatus == statusFilter);

            if (!string.IsNullOrEmpty(brokerSearch))
                query = query.Where(t => t.Brokername != null && t.Brokername.Contains(brokerSearch));

            // Sorting
            query = sortBy switch
            {
                "Price" => descending ? query.OrderByDescending(t => t.Priceperunit) : query.OrderBy(t => t.Priceperunit),
                "Quantity" => descending ? query.OrderByDescending(t => t.Quantity) : query.OrderBy(t => t.Quantity),
                "Broker" => descending ? query.OrderByDescending(t => t.Brokername) : query.OrderBy(t => t.Brokername),
                _ => descending ? query.OrderByDescending(t => t.Tradedate) : query.OrderBy(t => t.Tradedate),
            };

            // Pagination
            var totalItems = await query.CountAsync();
            var trades = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!trades.Any()) return NotFound("No trades found.");

            var response = new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Items = trades
            };

            return Ok(response);
        }
    }
}
