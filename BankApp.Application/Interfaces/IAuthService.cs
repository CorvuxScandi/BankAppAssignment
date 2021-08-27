﻿using BankApp.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUser(LoginDTO userLogin);

        Task<string> CreateToken();
    }
}