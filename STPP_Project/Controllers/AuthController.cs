using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STPP_Project.Auth;
using STPP_Project.Auth.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace STPP_Project.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ProjectRestUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(UserManager<ProjectRestUser> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByNameAsync(registerUserDto.UserName);
            if (user != null)
                return BadRequest("Request invalid.");

            var newUser = new ProjectRestUser
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.UserName
            };
            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUserResult.Succeeded)
                return BadRequest("Could not create a user.");

            await _userManager.AddToRoleAsync(newUser, ProjectRoles.RegisteredUser);

            return CreatedAtAction(nameof(Register), new UserDto(newUser.Id, newUser.UserName, newUser.Email));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return BadRequest("User name or password is invalid.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return BadRequest("User name or password is invalid.");

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

            return Ok(new SuccessfulLoginDto(accessToken));
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            ProjectRestUser projectRestUser = await _userManager.FindByIdAsync(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
            var jti = await _userManager.GetAuthenticationTokenAsync(projectRestUser, "JWT", "JWT Token");
            if (jti != User.FindFirstValue(JwtRegisteredClaimNames.Jti))
                return Unauthorized();

            await _userManager.RemoveAuthenticationTokenAsync(projectRestUser, "JWT", "JWT Token");

            return Ok();
        }
    }
}
