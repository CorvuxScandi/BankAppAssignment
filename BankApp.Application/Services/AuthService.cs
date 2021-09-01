using BankApp.Application.Interfaces;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        private ApplicationUser _user;
        private IRepository<Customer> _customerRepo;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IRepository<Customer> customerRepo)
        {
            _userManager = userManager;
            _configuration = configuration;
            _customerRepo = customerRepo;
        }

        public async Task<bool> ValidateUser(LoginDTO userLogin)
        {
            _user = await _userManager.FindByNameAsync(userLogin.UserName);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, userLogin.Password));
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();

            var claims = await GetClaims(
                _customerRepo.GetAll().ToList().First(x => x.Emailaddress == _user.UserName).CustomerId);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
                (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: credentials
                );

            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims(int customerId)
        {
            var roles = await _userManager.GetRolesAsync(_user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            if (roles.First() == UserRoles.User) claims.Add(new Claim("customerid", customerId.ToString()));

            return claims;
        }
    }
}