using STPP_Project.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace STPP_Project.Data.Entities
{
    public class City : IUserOwnedResource
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        public ProjectRestUser User { get; set; }
    }
}
