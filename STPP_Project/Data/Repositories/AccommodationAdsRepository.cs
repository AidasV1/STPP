using STPP_Project.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace STPP_Project.Data.Repositories
{
    public interface IAccommodationAdsRepository
    {
        Task<IReadOnlyList<AccommodationAd>> GetManyAsync(int cityId);
        Task<AccommodationAd?> GetAsync(int cityId, int accommodationAdId);
        Task CreateAsync(AccommodationAd accommodationAd);
        Task UpdateAsync(AccommodationAd accommodationAd);
        Task DeleteAsync(AccommodationAd accommodationAd);
    }

    public class AccommodationAdsRepository : IAccommodationAdsRepository
    {
        private readonly ProjectDbContext _projectDbContext;
        public AccommodationAdsRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<IReadOnlyList<AccommodationAd>> GetManyAsync(int cityId)
        {
            return await _projectDbContext.AccommodationAds.Where(o => o.City.Id == cityId).ToListAsync();
        }

        public async Task<AccommodationAd?> GetAsync(int cityId, int accommodationAdId)
        {
            return await _projectDbContext.AccommodationAds.FirstOrDefaultAsync(o => o.City.Id == cityId && o.Id == accommodationAdId);
        }

        public async Task CreateAsync(AccommodationAd accommodationAd)
        {
            _projectDbContext.AccommodationAds.Add(accommodationAd);
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AccommodationAd accommodationAd)
        {
            _projectDbContext.AccommodationAds.Update(accommodationAd);
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AccommodationAd accommodationAd)
        {
            _projectDbContext.AccommodationAds.Remove(accommodationAd);
            await _projectDbContext.SaveChangesAsync();
        }
    }
}
