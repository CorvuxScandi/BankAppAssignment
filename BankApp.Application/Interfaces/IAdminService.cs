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

        List<AccountType> AccountTypes();

        List<CustomerDTO> GetCostummers();

        List<AccountDTO> GetCustomerAccounts(int id);

        ApplicationResponce AddLoan(LoanDTO loan);

        ApplicationResponce AddNewCustomerProfile(RegisterModel customerModel);

        Task<ApplicationResponce> UpdateUserLogin(RegisterModel registerModel);
    }
}