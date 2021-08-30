using BankApp.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using BankApp.Enteties.DataTransferObjects;
using Newtonsoft.Json;
using BankApp.Enteties.Models.RequestFeatures;

namespace BankApp.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _baseUrl = "https://localhost:5000/api/customer/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CustomerView()
        {
            CustomerInfoDTO customer = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responce = await client.GetAsync("");

                if (responce.IsSuccessStatusCode)
                {
                    var apiResponce = responce.Content.ReadAsStringAsync().Result;

                    customer = JsonConvert.DeserializeObject<CustomerInfoDTO>(apiResponce);
                }
            }

            return View(customer);
        }

        public async Task<IActionResult> GetTransactions(TransactionParameters parameters)
        {
            PagedList<TransactionDTO> transactions;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responce = 
                    await client.GetAsync(
                    $"transactions?AccountID={parameters.AccountId}&PageNumber={parameters.PageNumber}&PageSize={parameters.PageSize}"
                    );

                if (responce.IsSuccessStatusCode)
                {
                    var apiResponce = responce.Content.ReadAsStringAsync().Result;

                    transactions = JsonConvert.DeserializeObject<PagedList<TransactionDTO>>(apiResponce);
                    return ViewComponent("Transactions", transactions);

                }
            }

            ViewBag.ErrorMessage = "Something went wrong, please try again";
            return RedirectToAction("CustoemrView");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}