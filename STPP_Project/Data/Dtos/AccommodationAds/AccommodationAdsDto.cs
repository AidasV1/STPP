namespace STPP_Project.Data.Dtos.AccommodationAds
{
    public record AccommodationAdDto(int Id, double Price, int BedCount);
    public record CreateAccommodationAdDto(double Price, int BedCount);
    public record UpdateAccommodationAdDto(double Price);
}
