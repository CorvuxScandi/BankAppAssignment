﻿using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Frontend.ViewComponents
{
    public class AddCustomerLoginViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            RegristrationDTO model = new();

            return View(model);
        }
    }
}