using ChartTestApi.Data;
using ChartTestApi.Helpers;
using ChartTestApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChartTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class RoutePointController : ControllerBase
    {
        private readonly ILogger<RoutePointController> _logger;
        private readonly ApiDbContext _context;

        /// <summary>
        /// When server is deploying in docker we need to be sure that database was created.
        /// It impossible to check it in Startup because at docker deploying
        /// we do not have correct db server until full deploy of all instances is not over.
        /// </summary>
        private static bool isCreated;

        /// <summary>        /// 
        /// So here is entry point to migrate database if it was not exist.
        /// </summary>
        private void MigrateDatabaseIfNotExist()
        {
            if (!isCreated)
            {
                isCreated = true;

                _logger.LogInformation("Database migration started.");

                _context.Database.Migrate();

                _logger.LogInformation("Database migrated successfully.");

                // fill database if it did not exist
                if (!_context.Tracks.Any())
                {
                    _logger.LogInformation("Database was not existed. Seed it.");
                    DbContextSeeder.Seed(_context);
                }
            }
        }

        public RoutePointController(
            ILogger<RoutePointController> logger,
            ApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet(Name = "points")]
        public async Task<ActionResult<IEnumerable<RoutePoint>>> Get()
        {
            MigrateDatabaseIfNotExist();
            var points = await _context.RoutePoints.ToListAsync();
            return Ok(points);
        }
    }


}
