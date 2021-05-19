using BankApp.Application.ApiModels;
using BankApp.Domain.DomainModels;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.Application.Interfaces
{
    public interface IAdminService
    {
        ApplicationResponce AddAccountType(AccountType accountType);

        List<Customer> GetCostummers();

        List<Account> GetAccounts();

        ApplicationResponce AddLoan(Loan loan);

        Task<ApplicationResponce> AddNewCustomerProfile(BankCustomerModel customerModel);

        Task<ApplicationResponce> UpdateUserLogin(Customer customer, RegisterModel registerModel);

    }
}
