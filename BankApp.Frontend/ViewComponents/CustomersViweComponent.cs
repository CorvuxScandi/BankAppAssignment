using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using BankApp.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankApp.Frontend.ViewComponents
{
    public class CustomersViweComponent : ViewComponent
    {
        private readonly IClientService _clientService;

        public CustomersViweComponent(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CustomerParameters p)
        {
            PagedList<CustomerDTO> pagedList = null;
            var resp = await _clientService.CallAPI("get", "admin", $"?PageNumber={p.PageNumber}&PageSize={p.PageSize}");

            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                pagedList = JsonConvert.DeserializeObject<PagedList<CustomerDTO>>(json);
            }

            return View("Customers", pagedList);
        }
    }
}