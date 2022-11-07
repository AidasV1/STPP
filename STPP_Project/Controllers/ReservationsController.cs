using Microsoft.AspNetCore.Mvc;
using STPP_Project.Data.Entities;
using STPP_Project.Data.Dtos.Reservations;
using STPP_Project.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using STPP_Project.Auth.Model;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace STPP_Project.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/ads/{accommodationAdId}/reservations")]
    public class ReservationsController :ControllerBase
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IAccommodationAdsRepository _accommodationAdsRepository;
        private readonly ICitiesRepository _citiesRepository;
        private readonly IAuthorizationService _authorizationService;
        public ReservationsController(IReservationsRepository reservationsRepository, IAccommodationAdsRepository accommodationAdsRepository, ICitiesRepository citiesRepository, IAuthorizationService authorizationService)
        {
            _reservationsRepository = reservationsRepository;
            _accommodationAdsRepository = accommodationAdsRepository;
            _citiesRepository = citiesRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<ReservationDto>> GetMany(int cityId, int accommodationAdId)
        {
            var reservations = await _reservationsRepository.GetManyAsync(cityId, accommodationAdId);

            return reservations.Select(o => new ReservationDto(o.Id, o.GuestCount, o.StartDate, o.EndDate));
        }

        [HttpGet]
        [Route("{reservationId}", Name = "GetReservation")]
        public async Task<ActionResult<ReservationDto>> Get(int cityId, int accommodationAdId, int reservationId)
        {
            var city = await _citiesRepository.GetAsync(cityId);
            var accommodationAd = await _accommodationAdsRepository.GetAsync(cityId, accommodationAdId);
            var reservation = await _reservationsRepository.GetAsync(cityId, accommodationAdId, reservationId);

            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }
            else if (accommodationAd == null)
            {
                return NotFound($"Couldn't find an ad with ID: {accommodationAdId}");
            }
            else if (reservation == null)
            {
                return NotFound($"Couldn't find a reservation with ID: {reservationId}");
            }

            return new ReservationDto(reservation.Id, reservation.GuestCount, reservation.StartDate, reservation.EndDate);
        }

        [HttpPost]
        [Authorize(Roles = ProjectRoles.RegisteredUser + ", " + ProjectRoles.Admin)]
        public async Task<ActionResult<ReservationDto>> Create(int cityId, int accommodationAdId, CreateReservationDto createReservationDto)
        {
            var city = await _citiesRepository.GetAsync(cityId);
            var accommodationAd = await _accommodationAdsRepository.GetAsync(cityId, accommodationAdId);

            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }
            else if (accommodationAd == null)
            {
                return NotFound($"Couldn't find an ad with ID: {accommodationAdId}");
            }

            var reservation = new Reservation
            {
                GuestCount = createReservationDto.GuestCount,
                StartDate = createReservationDto.StartDate,
                EndDate = createReservationDto.EndDate,
                AccommodationAd = accommodationAd,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            await _reservationsRepository.CreateAsync(reservation);

            return Created("", new ReservationDto(reservation.Id, reservation.GuestCount, reservation.StartDate, reservation.EndDate));
        }

        [HttpPut]
        [Route("{reservationId}")]
        [Authorize(Roles = ProjectRoles.RegisteredUser + ", " + ProjectRoles.Admin)]
        public async Task<ActionResult<ReservationDto>> Update(int cityId, int accommodationAdId, int reservationId, UpdateReservationDto updateReservationDto)
        {
            var city = await _citiesRepository.GetAsync(cityId);
            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }

            var accommodationAd = await _accommodationAdsRepository.GetAsync(cityId, accommodationAdId);
            if (accommodationAd == null)
            {
                return NotFound($"Couldn't find an ad with ID: {accommodationAdId}");
            }

            var reservation = await _reservationsRepository.GetAsync(cityId, accommodationAdId, reservationId);
            if (reservation == null)
            {
                return NotFound($"Couldn't find a reservation with ID: {reservationId}");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, reservation, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            reservation.StartDate = updateReservationDto.StartDate;
            reservation.EndDate = updateReservationDto.EndDate;
            await _reservationsRepository.UpdateAsync(reservation);

            return Ok(new ReservationDto(reservation.Id, reservation.GuestCount, reservation.StartDate, reservation.EndDate));
        }

        [HttpDelete]
        [Route("{reservationId}")]
        [Authorize(Roles = ProjectRoles.RegisteredUser + ", " + ProjectRoles.Admin)]
        public async Task<ActionResult> Remove(int cityId, int accommodationAdId, int reservationId)
        {
            var city = await _citiesRepository.GetAsync(cityId);

            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }

            var accommodationAd = await _accommodationAdsRepository.GetAsync(cityId, accommodationAdId);
            if (accommodationAd == null)
            {
                return NotFound($"Couldn't find an ad with ID: {accommodationAdId}");
            }

            var reservation = await _reservationsRepository.GetAsync(cityId, accommodationAdId, reservationId);
            if (reservation == null)
            {
                return NotFound($"Couldn't find a reservation with ID: {reservationId}");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, reservation, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _reservationsRepository.DeleteAsync(reservation);

            return NoContent();
        }
    }
}
