using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using BankApp.Frontend.Models;
using BankApp.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankApp.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientService _clientService;

        public HomeController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CustomerView()
        {
            string customerId = "";
            CustomerInfoDTO customer = new();

            HttpResponseMessage responce = await _clientService.CallAPI("get", "customer", customerId);

            if (responce.IsSuccessStatusCode)
            {
                var apiResponce = responce.Content.ReadAsStringAsync().Result;

                customer = JsonConvert.DeserializeObject<CustomerInfoDTO>(apiResponce);
            }

            return View(customer);
        }

        public async Task<IActionResult> GetTransactions(TransactionParameters parameters)
        {
            PagedList<TransactionDTO> transactions;

            HttpResponseMessage responce = await _clientService.CallAPI("get", "customer", $"transactions?AccountID={parameters.AccountId}&PageNumber={parameters.PageNumber}&PageSize={parameters.PageSize}");

            if (responce.IsSuccessStatusCode)
            {
                var apiResponce = responce.Content.ReadAsStringAsync().Result;

                transactions = JsonConvert.DeserializeObject<PagedList<TransactionDTO>>(apiResponce);
                return ViewComponent("Transactions", transactions);
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