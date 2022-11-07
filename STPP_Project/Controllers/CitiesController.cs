using Microsoft.AspNetCore.Mvc;
using STPP_Project.Data.Entities;
using STPP_Project.Data.Dtos.Cities;
using STPP_Project.Data.Dtos.Reservations;
using STPP_Project.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using STPP_Project.Auth.Model;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.EntityFrameworkCore;
using STPP_Project.Data.Dtos;
using STPP_Project.Auth;

namespace STPP_Project.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesRepository _citiesRepository;
        private readonly IAuthorizationService _authorizationService;
        public CitiesController(ICitiesRepository citiesRepository, IAuthorizationService authorizationService)
        {
            _citiesRepository = citiesRepository;
            _authorizationService = authorizationService;
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
        [Authorize(Roles = ProjectRoles.Admin)]
        public async Task<ActionResult<CityDto>> Create(CreateCityDto createCityDto)
        {
            var city = new City
            {
                Name = createCityDto.Name,
                Description = createCityDto.Description,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            await _citiesRepository.CreateAsync(city);

            return Created("", new CityDto(city.Id, city.Name, city.Description));
        }

        [HttpPut]
        [Route("{cityId}")]
        [Authorize(Roles = ProjectRoles.Admin)]
        public async Task<ActionResult<CityDto>> Update(int cityId, UpdateCityDto updateCityDto)
        {
            var city = await _citiesRepository.GetAsync(cityId);

            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, city, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            city.Description = updateCityDto.Description;
            await _citiesRepository.UpdateAsync(city);

            return Ok(new CityDto(city.Id, city.Name, city.Description));
        }

        [HttpDelete]
        [Route("{cityId}")]
        [Authorize(Roles = ProjectRoles.Admin)]
        public async Task<ActionResult> Remove(int cityId)
        {
            var city = await _citiesRepository.GetAsync(cityId);

            if (city == null)
            {
                return NotFound($"Couldn't find a city with ID: {cityId}");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, city, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _citiesRepository.DeleteAsync(city);

            return NoContent();
        }
    }
}
