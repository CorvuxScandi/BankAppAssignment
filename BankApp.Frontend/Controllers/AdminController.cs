using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using BankApp.Frontend.Models;
using BankApp.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Frontend.Controllers
{
    public class AdminController : Controller
    {
        private readonly IClientService _clientService;

        public AdminController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Index(CustomerParameters para)
        {
            string query = "admin?pageNumber=1&pageSize=20";
            if (para.PageSize != 10)
            {
                query = $"admin?pageNumber={para.PageNumber}&pageSize={para.PageSize}";
            }
            var resp = await _clientService.CallAPI(query);
            if (resp.IsSuccessStatusCode)
            {
                PagedList<CustomerDTO> customers;
                var contentString = resp.Content.ReadAsStringAsync().Result;

                var customerList = JsonConvert.DeserializeObject<List<CustomerDTO>>(contentString);
                resp.Headers.TryGetValues("X-Pagnation", out var jsonMeta);
                var metaData = JsonConvert.DeserializeObject<MetaData>(jsonMeta.FirstOrDefault());
                customers = new(customerList, customerList.Count, metaData.CurrentPage, metaData.PageSize);

                var metaString = JsonConvert.SerializeObject(metaData);
                HttpContext.Session.SetString("AdminMetaData", metaString);
                ViewData["Customers"] = customers;
                return View("AdminBoardView");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult PrevCustomers()
        {
            var metaData = JsonConvert.DeserializeObject<MetaData>(HttpContext.Session.GetString("AdminMetaData"));
            CustomerParameters para = new()
            {
                PageNumber = metaData.CurrentPage - 1,
                PageSize = metaData.PageSize
            };

            return RedirectToAction("Index", para);
        }

        public IActionResult NextCustomers()
        {
            var metaData = JsonConvert.DeserializeObject<MetaData>(HttpContext.Session.GetString("AdminMetaData"));

            CustomerParameters para = new()
            {
                PageNumber = metaData.CurrentPage + 1,
                PageSize = metaData.PageSize
            };

            return RedirectToAction("Index", para);
        }

        public async Task<IActionResult> Loan(int id)
        {
            var result = await _clientService.CallAPI($"admin/newloan/{id}");

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var resObj = JsonConvert.DeserializeObject<CustomerAndAccounts>(content);
                GiveLoanViewModel model = new()
                {
                    Accounts = resObj.Accounts,
                    Customer = resObj.Customer,
                    LoanDTO = new()
                };
                return View("GiveLoan", model);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GiveLoan(LoanDTO loan)
        {
            var result = await _clientService.CallAPI("admin/newloan", loan);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("GiveLoan", loan);
        }

        public async Task<IActionResult> RegAccount(NewAccountViewModel model)
        {
            var result = await _clientService.CallAPI("admin/newaccount", model.Account);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> NewCustomer(CustomerDTO customer)
        {
            var result = await _clientService.CallAPI("admin/newcustomer", customer);

            if (result.IsSuccessStatusCode)
            {
                RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}