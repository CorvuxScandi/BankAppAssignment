using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebApplication1.JwtAuthentication.AuthenticationServices
{
    public class CustomAuthenticationProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal claims = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.FromResult(0);
            return new(claims);
        }

        public void LoginNotify()
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.Email, "test@mail.com")
            },"FakeAuth");

            claims = new(identity);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public void LogoutNotify()
        {
            var anonymus = new ClaimsPrincipal(new ClaimsIdentity());
            claims = anonymus;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
