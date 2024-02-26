using ChartTestApi.Data;
using ChartTestApi.Helpers;
using ChartTestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChartTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly ILogger<TrackController> _logger;
        private readonly ApiDbContext _context;

        public TrackController(
            ILogger<TrackController> logger,
            ApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet(Name = "tracks")]
        public async Task<ActionResult<IEnumerable<Track>>> Get()
        {
            var points = await _context.Tracks.ToListAsync();
            return Ok(points);
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            await DbContextSeeder.Seed(_context);
            return Ok();
        }
    }
}
