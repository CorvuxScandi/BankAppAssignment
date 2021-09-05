using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Frontend.Services
{
    public class ClientService : IClientService
    {
        private readonly string _baseUrl = "https://localhost:5001/api/";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> CallAPI(string endURI, object value)
        {
            var jwtString = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri(_baseUrl);

            if (jwtString is not null)
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtString);
            }

            return await client.PostAsJsonAsync(endURI, value);
        }

        public async Task<HttpResponseMessage> CallAPI(string endURI)
        {
            var jwtString = _httpContextAccessor.HttpContext.Session.GetString("jwt");

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}{endURI}");

            using var client = new HttpClient();
            if (jwtString is not null)
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtString);
            }
            var response = await client.SendAsync(request);

            return response;
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            throw new NotImplementedException();
        }
    }
}