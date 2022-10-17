namespace STPP_Project.Data.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int GuestCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AccommodationAd AccommodationAd { get; set; }
    }
}
