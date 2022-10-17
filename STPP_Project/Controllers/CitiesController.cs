using Microsoft.AspNetCore.Mvc;
using STPP_Project.Data.Entities;
using STPP_Project.Data.Dtos.Cities;
using STPP_Project.Data.Dtos.Reservations;
using STPP_Project.Data.Repositories;

namespace STPP_Project.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesRepository _citiesRepository;
        public CitiesController(ICitiesRepository citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }

        [HttpGet]
        [Route("{cityId}/cityreservations")]
        public async Task<IEnumerable<ReservationDto>> GetCityReservations(int cityId)
        {
            var reservations = await _citiesRepository.GetCityReservations(cityId);

            return reservations.Select(o => new ReservationDto(o.Id, o.GuestCount, o.StartDate, o.EndDate));
        }

        [HttpGet]
        public async Task<IEnumerable<CityDto>> GetMany()
        {
            var cities = await _citiesRepository.GetManyAsync();

            return cities.Select(o => new CityDto(o.Id, o.Name, o.Description));
        }

        [HttpGet]
        [Route("{cityId}", Name = "GetCity")]
        public async Task<ActionResult<CityDto>> Get(int cityId)
        {
            var city = await _citiesRepository.GetAsync(cityId);

            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }

            return new CityDto(city.Id, city.Name, city.Description);
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> Create(CreateCityDto createCityDto)
        {
            var city = new City
            {
                Name = createCityDto.Name,
                Description = createCityDto.Description
            };

            await _citiesRepository.CreateAsync(city);

            return Created("", new CityDto(city.Id, city.Name, city.Description));
        }

        [HttpPut]
        [Route("{cityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityDto>> Update(int cityId, UpdateCityDto updateCityDto)
        {
            var city = await _citiesRepository.GetAsync(cityId);

            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }

            city.Description = updateCityDto.Description;
            await _citiesRepository.UpdateAsync(city);

            return Ok(new CityDto(city.Id, city.Name, city.Description));
        }

        [HttpDelete]
        [Route("{cityId}")]
        public async Task<ActionResult> Remove(int cityId)
        {
            var city = await _citiesRepository.GetAsync(cityId);

            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }

            await _citiesRepository.DeleteAsync(city);

            return NoContent();
        }
    }
}
