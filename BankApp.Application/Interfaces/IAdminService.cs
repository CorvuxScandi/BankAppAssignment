using BankApp.Application.ApiModels;
using BankApp.Domain.DomainModels;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Application.Interfaces
{
    public interface IAdminService
    {


        ApplicationResponce AddAccountType(AccountType accountType);

        ApplicationResponce AddNewCustomerProfile(BankCustomerModel customerModel, IdentityUser identity);

        ApplicationResponce UpdateCustomerProfile(BankCustomerModel customerModel, IdentityUser identity);

        ApplicationResponce FreezeAccount(BankCustomerModel account);

        ApplicationResponce FreezeCustomer(Customer customer);

        ApplicationResponce GetCustomerProfile(BankCustomerModel customerModel);
    }
}
