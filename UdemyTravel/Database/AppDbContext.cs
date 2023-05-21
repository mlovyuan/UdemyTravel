using Microsoft.EntityFrameworkCore;
using UdemyTravel.Models;

namespace UdemyTravel.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // variable name will be table name
        public DbSet<TouristRoute> TouristRoutes { get; set; }
        public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
    }
}
