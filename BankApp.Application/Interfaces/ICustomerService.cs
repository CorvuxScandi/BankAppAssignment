using BankApp.Application.ApiModels;
using BankApp.Domain.DomainModels;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Application.Interfaces
{
    public interface ICustomerService
    {
        ApplicationResponce GetAccountInfo(string authId);

        ApplicationResponce Addtransaction(Transaction transaction);

        ApplicationResponce GetTransactions(int accountId);
    }
}
