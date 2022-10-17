using STPP_Project.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace STPP_Project.Data
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<AccommodationAd> AccommodationAds { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=stpp;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=STPPDB");
        }
    }
}
