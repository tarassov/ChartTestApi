using ChartTestApi.Data;
using ChartTestApi.Enums;
using ChartTestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChartTestApi.Helpers
{
    public static class DbContextSeeder
    {
        public static async Task Seed(ApiDbContext context)
        {
            if (!context.Database.EnsureCreated())
            {
                var allPoints = new List<RoutePoint>();
                var firstPoint = new RoutePoint()
                {
                    Id = 1,
                    Name = "Начало приключений",
                    Height = 100,
                };
                var points = GeneratePoints();
                var lastPoint = new RoutePoint()
                {
                    Id = points.Count+2,
                    Name = "Конец маршрута",
                    Height = 100,
                };

                allPoints.Add(firstPoint);
                allPoints.AddRange(points);
                allPoints.Add(lastPoint);

                var tracks = GenerateTracks(allPoints);
                context.Tracks.ExecuteDelete();
                context.RoutePoints.ExecuteDelete();
                context.AddRange(allPoints);
                await context.AddRangeAsync(tracks);
                await context.SaveChangesAsync();       
            }
        }


        private static List<RoutePoint> GeneratePoints()
        {
            Random rnd = new Random();
            var points = new List<RoutePoint>();
            for (var i = 0; i < 30; i++) {
                
                var point = new RoutePoint()
                {
                    Id = 2 + i,
                    Name = $"Точка невозврата номер {2 + i}",
                    Height = rnd.Next(10, 200)
                };
                points.Add(point);
            }

            return points;
        }

        private static List<Track> GenerateTracks(List<RoutePoint> points)
        {
            var tracks = new List<Track>();
            for (var i=0; i<points.Count -1; i++)  {

                var point = points[i];
                tracks.Add(GenerateTrack(point.Id));                
            }
            return tracks;
        }

        private static Track GenerateTrack(int id)
        {
            Random rnd = new Random();
            return new Track()
            {
                Id=id,
                FirstId = id,
                SecondId = id + 1,
                Distance = (int)Math.Round(rnd.Next(100, 2000) / 50.0) * 50,
                MaxSpeed = (MaxSpeed)rnd.Next(0, 3),
                Surface = (Surface)rnd.Next(0, 3)
            };
        }
    }
}
