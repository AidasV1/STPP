using STPP_Project.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using STPP_Project.Auth.Model;

namespace STPP_Project.Data
{
    public class ProjectDbContext : IdentityDbContext<ProjectRestUser>
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<AccommodationAd> AccommodationAds { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=stpp;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=STPPDB");
        }
    }
}
