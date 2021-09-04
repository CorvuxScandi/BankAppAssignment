using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using BankApp.Frontend.Services;
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
        public IActionResult Index()
        {
            return View("AdminBoardView");
        }

        public IActionResult GetCustomers(CustomerParameters para)
        {
            if (para is null)
            {
                para = new()
                {
                    PageNumber = 1,
                    PageSize = 20
                };
            }

            return ViewComponent("Customers", new { p = para });
        }
    }
}