using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.Application.Interfaces
{
    public interface IAdminService
    {
        void AddAccountType(AccountType accountType);

        List<AccountType> GetAccountTypes();

        Task<PagedList<Customer>> GetCustomers(CustomerParameters parameters);

        List<Account> GetCustomerAccounts(int id);

        void AddLoan(Loan loan);

        void AddNewCustomerProfile(RegisterCustomerDTO customerModel);
    }
}