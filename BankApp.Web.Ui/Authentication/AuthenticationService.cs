using BankApp.Web.Ui.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankApp.Web.Ui.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authState;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client,
            AuthenticationStateProvider authState,
            ILocalStorageService storageService)
        {
            _client = client;
            _authState = authState;
            _localStorage = storageService;
        }

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel authenticationModel)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", authenticationModel.Username),
                new KeyValuePair<string, string>("password", authenticationModel.Password)
            });

            var authResult = await _client.PostAsync(_client.BaseAddress + "/authentication", data);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }
            var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(
                authContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await _localStorage.SetItemAsync("authToken", result.AccessToken);

            ((AuthStateProvider)_authState).NotifyUserAuthentication(result.AccessToken);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);
            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authState).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}