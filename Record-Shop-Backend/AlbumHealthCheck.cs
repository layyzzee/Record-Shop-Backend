using Microsoft.Extensions.Diagnostics.HealthChecks;
using Record_Shop_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Record_Shop_Backend
{
    public class AlbumHealthCheck : IHealthCheck
    {
        private readonly AlbumDbContext _context;

        public AlbumHealthCheck(AlbumDbContext albumDbContext)
        {
            _context = albumDbContext;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var albums = await _context.Albums.ToListAsync();

            int albumCount = albums.Count();

            if (albumCount > 0)
            {
                return HealthCheckResult.Healthy($"There are {albumCount} albums available.");
            }
            else
            {
                return HealthCheckResult.Unhealthy($"There are {albumCount} albums available.");
            }
        }
    }
}
