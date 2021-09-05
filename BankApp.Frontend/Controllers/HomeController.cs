using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using BankApp.Frontend.Models;
using BankApp.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            int customerId = HttpContext.Session.GetInt32("id").Value;

            var customerResp = await _clientService.CallAPI("get", "customer/", customerId.ToString());
            var accountResp = await _clientService.CallAPI("get", "admin/", "accounttypes");

            if (customerResp.IsSuccessStatusCode && accountResp.IsSuccessStatusCode)
            {
                var apiResponce = customerResp.Content.ReadAsStringAsync().Result;
                var customerInfo = JsonConvert.DeserializeObject<CustomerInfoDTO>(apiResponce);
                var respContent = await accountResp.Content.ReadAsStringAsync();

                ViewData["Customer"] = customerInfo.CustomerInfo;
                ViewData["Accounts"] = customerInfo.Accounts;
                ViewData["Cards"] = customerInfo.Cards;
                ViewData["Loans"] = customerInfo.Loans;
                ViewData["AccountTypes"] = JsonConvert.DeserializeObject<List<AccountTypeDTO>>(respContent);
            }

            return View("CustomerView");
        }

        public async Task<IActionResult> GetTransactions(TransactionParameters para)
        {
            PagedList<TransactionDTO> transactions;
            string query = $"transactions?accountid={para.AccountId}&pagenumber={para.PageNumber}&pagesize={para.PageSize}";

            var transactionResp = await _clientService.CallAPI("get", "customer/", query);
            if (transactionResp.IsSuccessStatusCode)
            {
                var transactionList = JsonConvert.DeserializeObject<List<TransactionDTO>>(transactionResp.Content.ReadAsStringAsync().Result);
                transactionResp.Headers.TryGetValues("X-Pagnation", out var jsonMeta);
                var metaData = JsonConvert.DeserializeObject<MetaData>(jsonMeta.FirstOrDefault());
                transactions = new(transactionList, transactionList.Count, metaData.CurrentPage, metaData.PageSize);
                transactions.MetaData.TotalPages = metaData.TotalPages;

                var metaString = JsonConvert.SerializeObject(metaData);
                HttpContext.Session.SetString("MetaData", metaString);

                return PartialView("_TransactionsPartial", transactions);
            };

            ViewBag.errorMessage = "server error";
            transactions = new(new(), 0, 1, 1);
            return PartialView("_TransactionsPartial", transactions);
        }

        public IActionResult PrevTransactions()
        {
            var metaData = JsonConvert.DeserializeObject<MetaData>(HttpContext.Session.GetString("MetaData"));
            var id = HttpContext.Session.GetInt32("AccountId").Value;
            TransactionParameters para = new()
            {
                AccountId = id,
                PageNumber = metaData.CurrentPage - 1,
                PageSize = metaData.PageSize
            };

            return RedirectToAction("GetTransactions", para);
        }

        public IActionResult NextTransactions()
        {
            var metaData = JsonConvert.DeserializeObject<MetaData>(HttpContext.Session.GetString("MetaData"));
            var id = HttpContext.Session.GetInt32("AccountId").Value;

            TransactionParameters para = new()
            {
                AccountId = id,
                PageNumber = metaData.CurrentPage + 1,
                PageSize = metaData.PageSize
            };

            return RedirectToAction("GetTransactions", para);
        }

        public IActionResult InitiateTransactions(int accountId)
        {
            HttpContext.Session.SetInt32("AccountId", accountId);
            TransactionParameters para = new()
            {
                AccountId = accountId,
                PageNumber = 1,
                PageSize = 30
            };

            return RedirectToAction("GetTransactions", para);
        }

        public IActionResult MakeTransaction(int accountId)
        {
            TransactionDTO transactionDTO = new() { AccountId = accountId };

            return View(transactionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Transaction(TransactionDTO transaction)
        {
            var resp = await _clientService.CallAPI("post", "customer", "/transaction", transaction);
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerView");
            }

            ViewBag.ErrorMessage = "server error try again";
            return View("MakeTransaction", transaction);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}