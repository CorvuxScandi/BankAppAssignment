using BankApp.Web.Ui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace BankApp.Web.Ui.Services
{
    public class CustomerServices
    {
        private readonly HttpClient _httpClient;

        public CustomerDetails CustomerInfo { get; private set; }
        public List<TransferDTO> Transactions { get; private set; }

        public event Action OnChange;

        public CustomerServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void StateChanged()
        {
            OnChange?.Invoke();
        }

        public async void GetDetails()
        {
            CustomerInfo = await _httpClient.GetFromJsonAsync<CustomerDetails>("api/customer/profile");
            StateChanged();
        }

        public async void GetTransactions(int accountId)
        {
            Transactions = await _httpClient.GetFromJsonAsync<List<TransferDTO>>("api/customer/accounts?id=" + accountId);
            StateChanged();
        }

        public async void InternalTransaction(InternalTransaction transaction)
        {
            await _httpClient.PostAsJsonAsync("api/customer/internal", transaction);
        }
    }
}