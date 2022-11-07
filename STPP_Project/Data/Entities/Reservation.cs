using STPP_Project.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace STPP_Project.Data.Entities
{
    public class Reservation : IUserOwnedResource
    {
        [Key]
        public int Id { get; set; }
        public int GuestCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AccommodationAd AccommodationAd { get; set; }
        [Required]
        public string UserId { get; set; }
        public ProjectRestUser User { get; set; }
    }
}
