using BankApp.Application.ApiModels;
using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.UI.WebApp.Services
{
    public interface ICustomerService
    {
        Task<BankCustomerModel> GetCustomerInformation();

        Task<IEnumerable<Transaction>> GetTransactions();
    }
}
