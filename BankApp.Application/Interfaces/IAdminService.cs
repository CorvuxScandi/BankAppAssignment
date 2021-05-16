using BankApp.Application.ApiModels;
using BankApp.Domain.DomainModels;
using BankApp.Domain.Models;

namespace BankApp.Application.Interfaces
{
    public interface IAdminService
    {


        ApplicationResponce AddAccountType(AccountType accountType);

        ApplicationResponce AddNewCustomerProfile(BankCustomerModel customerModel);

        ApplicationResponce UpdateCustomerProfile(BankCustomerModel customerModel);

        ApplicationResponce FreezeAccount(BankCustomerModel account);

        ApplicationResponce FreezeCustomer(Customer customer);

        ApplicationResponce GetCustomerProfile(BankCustomerModel customerModel);
    }
}
