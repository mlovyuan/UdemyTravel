using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json.Serialization;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // create seed data method 1
            //modelBuilder.Entity<TouristRoute>().HasData(new TouristRoute()
            //{
            //    Id = Guid.NewGuid(),
            //    Title = "SampleTitle",
            //    Description = "SampleDescription",
            //    OriginalPrice = 0,
            //    CreateTime = DateTime.UtcNow,
            //});

            // create seed data method 2
            var touristRouteJsonData = File.ReadAllText($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Database/touristRoutesMockData.json");
            var touristRoutes = JsonConvert.DeserializeObject<IList<TouristRoute>>(touristRouteJsonData);
            modelBuilder.Entity<TouristRoute>().HasData(touristRoutes);

            var touristRoutePictureJsonData = File.ReadAllText($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Database/touristRoutePicturesMockData.json");
            var touristRoutePictures = JsonConvert.DeserializeObject<IList<TouristRoutePicture>>(touristRoutePictureJsonData);
            modelBuilder.Entity<TouristRoutePicture>().HasData(touristRoutePictures);
            base.OnModelCreating(modelBuilder);
        }
    }
}
