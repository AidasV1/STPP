using STPP_Project.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace STPP_Project.Data.Repositories
{
    public interface IReservationsRepository
    {
        Task<IReadOnlyList<Reservation>> GetManyAsync(int cityId, int accommodationAdId);
        Task<Reservation?> GetAsync(int cityId, int accommodationAdId, int reservationId);
        Task CreateAsync(Reservation reservation);
        Task DeleteAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
    }

    public class ReservationsRepository : IReservationsRepository
    {
        private readonly ProjectDbContext _projectDbContext;
        public ReservationsRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<IReadOnlyList<Reservation>> GetManyAsync(int cityId, int accommodationAdId)
        {
            return await _projectDbContext.Reservations.Where(o => o.AccommodationAd.City.Id == cityId && o.AccommodationAd.Id == accommodationAdId).ToListAsync();
        }

        public async Task<Reservation?> GetAsync(int cityId, int accommodationAdId, int reservationId)
        {
            return await _projectDbContext.Reservations.FirstOrDefaultAsync(o => o.AccommodationAd.City.Id == cityId && o.AccommodationAd.Id == accommodationAdId && o.Id == reservationId);
        }

        public async Task CreateAsync(Reservation reservation)
        {
            _projectDbContext.Reservations.Add(reservation);
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _projectDbContext.Reservations.Update(reservation);
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _projectDbContext.Reservations.Remove(reservation);
            await _projectDbContext.SaveChangesAsync();
        }
    }
}
