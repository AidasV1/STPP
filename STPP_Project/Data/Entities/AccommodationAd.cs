namespace STPP_Project.Data.Entities
{
    public class AccommodationAd
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int BedCount { get; set; }
        public City City { get; set; }
    }
}
