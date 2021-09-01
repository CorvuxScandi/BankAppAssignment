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
        private readonly string _baseUrl = "https://localhost:5000/api/";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> CallAPI(string method, string target, string endURI, object value)
        {
            var jwtString = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

            client.BaseAddress = new Uri(_baseUrl + target);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (jwtString is not null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtString);
            }

            return await client.PostAsJsonAsync(endURI, value);
        }

        public async Task<HttpResponseMessage> CallAPI(string method, string target, string endURI)
        {
            var jwtString = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

            client.BaseAddress = new Uri(_baseUrl + target);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (jwtString is not null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtString);
            }

            return await client.GetAsync(endURI);
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            throw new NotImplementedException();
        }
    }
}