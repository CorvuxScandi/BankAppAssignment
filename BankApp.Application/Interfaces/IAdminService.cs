using BankApp.Application.ApiModels;
using BankApp.Domain.DomainModels;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Models;
using System.Threading.Tasks;

namespace BankApp.Application.Interfaces
{
    public interface IAdminService
    {


        ApplicationResponce AddAccountType(AccountType accountType);

        Task<ApplicationResponce> AddNewCustomerProfile(BankCustomerModel customerModel, RegisterModel registerModel);

        Task<ApplicationResponce> UpdateUserLogin(Customer customer, RegisterModel registerModel);

        ApplicationResponce FreezeAccount(BankCustomerModel account);

        ApplicationResponce FreezeCustomer(Customer customer);

        ApplicationResponce GetCustomerProfile(int id);
    }
}
