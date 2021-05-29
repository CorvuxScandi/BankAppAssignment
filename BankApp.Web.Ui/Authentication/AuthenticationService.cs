﻿using BankApp.Web.Ui.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankApp.Web.Ui.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client,
            AuthenticationStateProvider authStateProvider,
            ILocalStorageService storageService)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = storageService;
        }

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel authenticationModel)
        {
            var content = JsonSerializer.Serialize(authenticationModel);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync("https://localhost:5008/api/authentication/login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (!authResult.IsSuccessStatusCode)
            {
                return result;
            }
            await _localStorage.SetItemAsync("authToken", result.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(authenticationModel.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return new AuthenticatedUserModel { IsAuthSuccessful = true };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}