using BankApp.Web.Ui.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace BankApp.Web.Ui.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly HttpClient _httpClient;
        public AccountDTO Account { get; set; }
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

        public async void GetTransactions()
        {
            Transactions = await _httpClient.GetFromJsonAsync<List<TransferDTO>>("api/customer/accounts?id=" + Account.AccountId);
            StateChanged();
        }

        public void SelectedAccount(int accountId)
        {
            Account = CustomerInfo.Accounts.Find(a => a.AccountId == accountId);
            StateChanged();
        }

        public async void InternalTransaction(InternalTransaction transaction)
        {
            await _httpClient.PostAsJsonAsync("api/customer/internal", transaction);
        }
    }
}