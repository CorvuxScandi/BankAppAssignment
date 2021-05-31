using BankApp.Application.ApiModels;
using BankApp.Domain.DomainModels;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Application.Interfaces
{
    public interface ICustomerService
    {
        ApplicationResponce GetCustomerInfo(string email);

        ApplicationResponce Addtransaction(InternalTransaction transaction);

        ApplicationResponce GetTransactions(int accountId);
    }
}