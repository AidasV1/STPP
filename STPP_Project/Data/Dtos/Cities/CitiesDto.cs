namespace STPP_Project.Data.Dtos.Cities
{
    public record CityDto(int Id, string Name, string Description);
    public record CreateCityDto(string Name, string Description);
    public record UpdateCityDto(string Description);
}
