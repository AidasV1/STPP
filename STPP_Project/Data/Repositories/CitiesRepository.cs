using STPP_Project.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace STPP_Project.Data.Repositories
{
    public interface ICitiesRepository
    {
        Task<IReadOnlyList<Reservation>> GetCityReservations(int cityId);
        Task<IReadOnlyList<City>> GetManyAsync();
        Task<City?> GetAsync(int cityId);
        Task CreateAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(City city);
    }

    public class CitiesRepository : ICitiesRepository
    {
        private readonly ProjectDbContext _projectDbContext;
        public CitiesRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<IReadOnlyList<Reservation>> GetCityReservations(int cityId)
        {
            return await _projectDbContext.Reservations
                .AsNoTracking()
                .Include(o => o.AccommodationAd)
                .ThenInclude(o => o.City)
                .Where(o => o.AccommodationAd.City.Id == cityId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<City>> GetManyAsync()
        {
            return await _projectDbContext.Cities.ToListAsync();
        }

        public async Task<City?> GetAsync(int cityId)
        {
            return await _projectDbContext.Cities.FirstOrDefaultAsync(o => o.Id == cityId);
        }

        public async Task CreateAsync(City city)
        {
            _projectDbContext.Cities.Add(city);
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(City city)
        {
            _projectDbContext.Cities.Update(city);
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(City city)
        {
            _projectDbContext.Cities.Remove(city);
            await _projectDbContext.SaveChangesAsync();
        }
    }
}
