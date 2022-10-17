using Microsoft.AspNetCore.Mvc;
using STPP_Project.Data.Entities;
using STPP_Project.Data.Dtos.AccommodationAds;
using STPP_Project.Data.Repositories;

namespace STPP_Project.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/ads")]
    public class AccommodationAdsController : ControllerBase
    {
        private readonly IAccommodationAdsRepository _accommodationAdsRepository;
        private readonly ICitiesRepository _citiesRepository;
        public AccommodationAdsController(IAccommodationAdsRepository accommodationAdsRepository, ICitiesRepository citiesRepository)
        {
            _accommodationAdsRepository = accommodationAdsRepository;
            _citiesRepository = citiesRepository;
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
                City = city
            };

            await _accommodationAdsRepository.CreateAsync(accommodationAd);

            return Created("", new AccommodationAdDto(accommodationAd.Id, accommodationAd.Price, accommodationAd.BedCount));
        }

        [HttpPut]
        [Route("{accommodationAdId}")]
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

            accommodationAd.Price = updateAccommodationDto.Price;
            await _accommodationAdsRepository.UpdateAsync(accommodationAd);

            return Ok(new AccommodationAdDto(accommodationAd.Id, accommodationAd.Price, accommodationAd.BedCount));
        }

        [HttpDelete]
        [Route("{accommodationAdId}")]
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

            await _accommodationAdsRepository.DeleteAsync(accommodationAd);

            return NoContent();
        }
    }
}
