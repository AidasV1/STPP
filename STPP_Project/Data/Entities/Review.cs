namespace STPP_Project.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public AccommodationAd AccommodationAd { get; set; }
        //public Reservation Reservation { get; set; }
    }
}
