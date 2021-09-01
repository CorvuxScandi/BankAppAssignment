using BankApp.Application.Interfaces;
using BankApp.Domain.IdentityModels;
using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankApp.Web.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IAuthService _authService;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        [HttpPost("register")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterUser([FromBody] RegristrationDTO regristration)
        {
            var user = new ApplicationUser()
            {
                UserName = regristration.Email,
                Email = regristration.Email
            };

            foreach (var role in regristration.Roles)
            {
                var doesExist = await _roleManager.RoleExistsAsync(role);
                if (!doesExist)
                {
                    ModelState.TryAddModelError("404", "Role does not exist in database");

                    return BadRequest(ModelState);
                };
            }

            var result = await _userManager.CreateAsync(user, regristration.Password);
            if (!result.Succeeded)
            {
                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, regristration.Roles);

            return StatusCode(201);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> GetToken([FromBody] LoginDTO user)
        {
            if (!await _authService.ValidateUser(user))
            {
                return Unauthorized();
            }

            return Ok(new { Token = await _authService.CreateToken() });
        }
    }
}