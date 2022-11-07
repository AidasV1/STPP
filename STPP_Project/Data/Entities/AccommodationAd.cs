using STPP_Project.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace STPP_Project.Data.Entities
{
    public class AccommodationAd : IUserOwnedResource
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public int BedCount { get; set; }
        public City City { get; set; }
        [Required]
        public string UserId { get; set; }
        public ProjectRestUser User { get; set; }
    }
}
