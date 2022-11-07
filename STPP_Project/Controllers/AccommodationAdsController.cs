using Microsoft.AspNetCore.Mvc;
using STPP_Project.Data.Entities;
using STPP_Project.Data.Dtos.AccommodationAds;
using STPP_Project.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using STPP_Project.Auth.Model;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace STPP_Project.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/ads")]
    public class AccommodationAdsController : ControllerBase
    {
        private readonly IAccommodationAdsRepository _accommodationAdsRepository;
        private readonly ICitiesRepository _citiesRepository;
        private readonly IAuthorizationService _authorizationService;
        public AccommodationAdsController(IAccommodationAdsRepository accommodationAdsRepository, ICitiesRepository citiesRepository, IAuthorizationService authorizationService)
        {
            _accommodationAdsRepository = accommodationAdsRepository;
            _citiesRepository = citiesRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<AccommodationAdDto>> GetMany(int cityId)
        {
            var accommodationAds = await _accommodationAdsRepository.GetManyAsync(cityId);

            return accommodationAds.Select(o => new AccommodationAdDto(o.Id, o.Price, o.BedCount));
        }

        [HttpGet]
        [Route("{accommodationAdId}", Name = "GetAccommodationAd")]
        public async Task<ActionResult<AccommodationAdDto>> Get(int cityId, int accommodationAdId)
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

            return new AccommodationAdDto(accommodationAd.Id, accommodationAd.Price, accommodationAd.BedCount);
        }

        [HttpPost]
        [Authorize(Roles = ProjectRoles.RegisteredUser + ", " + ProjectRoles.Admin)]
        public async Task<ActionResult<AccommodationAdDto>> Create(int cityId, CreateAccommodationAdDto createAccommodationAdDto)
        {
            var city = await _citiesRepository.GetAsync(cityId);

            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }

            var accommodationAd = new AccommodationAd
            {
                Price = createAccommodationAdDto.Price,
                BedCount = createAccommodationAdDto.BedCount,
                City = city,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            await _accommodationAdsRepository.CreateAsync(accommodationAd);

            return Created("", new AccommodationAdDto(accommodationAd.Id, accommodationAd.Price, accommodationAd.BedCount));
        }

        [HttpPut]
        [Route("{accommodationAdId}")]
        [Authorize(Roles = ProjectRoles.RegisteredUser + ", " + ProjectRoles.Admin)]
        public async Task<ActionResult<AccommodationAdDto>> Update(int cityId, int accommodationAdId, UpdateAccommodationAdDto updateAccommodationDto)
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

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, accommodationAd, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            accommodationAd.Price = updateAccommodationDto.Price;
            await _accommodationAdsRepository.UpdateAsync(accommodationAd);

            return Ok(new AccommodationAdDto(accommodationAd.Id, accommodationAd.Price, accommodationAd.BedCount));
        }

        [HttpDelete]
        [Route("{accommodationAdId}")]
        [Authorize(Roles = ProjectRoles.RegisteredUser + ", " + ProjectRoles.Admin)]
        public async Task<ActionResult> Remove(int cityId, int accommodationAdId)
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

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, accommodationAd, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _accommodationAdsRepository.DeleteAsync(accommodationAd);

            return NoContent();
        }
    }
}
