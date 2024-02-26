using ChartTestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChartTestApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        public DbSet<RoutePoint> RoutePoints { get; set; }
        public DbSet<Track> Tracks { get; set; }
    }
}
