using BankApp.Application.ApiModels;
using BankApp.Domain.Models;
using BankApp.UI.WebApp.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankApp.UI.WebApp.CustomerPages
{
    public class CustomerPages : ICustomerService
    {
        private readonly HttpClient _http;

        public CustomerPages(HttpClient http)
        {
            _http = http;
        }

        public async Task<BankCustomerModel> GetCustomerInformation()
        {
            var customerInfo = await _http.GetStreamAsync($"api/customer");
            return await JsonSerializer.DeserializeAsync<BankCustomerModel>
        }

        public Task<IEnumerable<Transaction>> GetTransactions()
        {
            throw new NotImplementedException();
        }
    }
}
