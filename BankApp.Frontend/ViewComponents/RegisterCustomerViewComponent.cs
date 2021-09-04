using BankApp.Enteties.DataTransferObjects;
using BankApp.Frontend.Models;
using BankApp.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Frontend.ViewComponents
{
    public class RegisterCustomerViewComponent : ViewComponent
    {
        private readonly IClientService _clientService;

        public RegisterCustomerViewComponent(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            NewCustomerViewModel model = new()
            {
                Account = new(),
                Customer = new(),
            };
            var resp = await _clientService.CallAPI("get", "admin", "/accounttypes");
            if (resp.IsSuccessStatusCode)
            {
                var jsonString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<List<AccountTypeDTO>>(jsonString);
                model.AccountTypes = json;

                return View(model);
            }

            return View();
        }
    }
}