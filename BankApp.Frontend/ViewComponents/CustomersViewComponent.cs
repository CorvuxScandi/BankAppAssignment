using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using BankApp.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankApp.Frontend.ViewComponents
{
    public class CustomersViewComponent : ViewComponent
    {
        private readonly IClientService _clientService;

        public CustomersViewComponent(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CustomerParameters p)
        {
            PagedList<CustomerDTO> pagedList;
            if (p is null)
            {
                p = new()
                {
                    PageNumber = 1,
                    PageSize = 30
                };
            }
            string url = $"admin?pageNumber={p.PageNumber}&pageSize={p.PageSize}";
            var resp = await _clientService.CallAPI(url);

            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                var customerList = JsonConvert.DeserializeObject<List<CustomerDTO>>(json);

                resp.Headers.TryGetValues("X-Pagnation", out var jsonMeta);
                var val = jsonMeta.FirstOrDefault();
                var metaData = JsonConvert.DeserializeObject<MetaData>(val);

                pagedList = new(customerList, customerList.Count, metaData.CurrentPage, metaData.PageSize);

                //pagedList = PagedList<CustomerDTO>.ToPagedList(customerList, metaData.CurrentPage, metaData.PageSize);

                return View(pagedList);
            }

            return View();
        }
    }
}