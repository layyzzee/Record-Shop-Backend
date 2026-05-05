using Microsoft.EntityFrameworkCore;
using Record_Shop_Backend.MVC_Data_Models;
namespace Record_Shop_Backend.Data
{
    public class AlbumDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public AlbumDbContext(DbContextOptions<AlbumDbContext> options) : base(options)
        {

        }
    }
}
