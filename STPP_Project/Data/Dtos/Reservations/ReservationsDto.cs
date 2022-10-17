namespace STPP_Project.Data.Dtos.Reservations
{
    public record ReservationDto(int Id, int GuestCount, DateTime StartDate, DateTime EndDate);
    public record CreateReservationDto(int GuestCount, DateTime StartDate, DateTime EndDate);
    public record UpdateReservationDto(DateTime StartDate, DateTime EndDate);
}
