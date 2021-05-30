using BankApp.Application.ApiModels;
using BankApp.Domain.DomainModels;
using BankApp.Domain.IdentityModels;
using BankApp.Web.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Web.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationUserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:securityKey"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:expiryInMinutes"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized(new AuthenticatedUserModel {ErrorMessage = "Invalid login" });
        }


        [HttpPost("newIdentity")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateIdentity([FromBody] IdentityRegModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return BadRequest(new ApplicationResponce { ResponceCode = 409, ResponceText = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
                
            };
            var result = await _userManager.CreateAsync(user, "!Sunshine1");
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResponceCode = 400, ResponceText = "User creation failed! Please check user details and try again." });

            
            if (model.NewAdmin)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            await _userManager.AddToRoleAsync(user, UserRoles.User);
 
            return Ok(new ApplicationResponce { ResponceCode = 201, ResponceText = "User created successfully!" });
        }

        
    }
}