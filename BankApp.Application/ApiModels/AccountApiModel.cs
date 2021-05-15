using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Application.ApiModels
{
    public class AccountApiModel
    {
        public IEnumerable<Account> Accounts { get; set; }
    }
}
