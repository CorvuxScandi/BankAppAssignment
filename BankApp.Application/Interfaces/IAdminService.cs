using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.Application.Interfaces
{
    public interface IAdminService
    {
        void AddAccountType(AccountTypeDTO accountType);

        List<AccountTypeDTO> GetAccountTypes();

        Task<PagedList<CustomerDTO>> GetCustomers(CustomerParameters parameters);

        List<AccountDTO> GetCustomerAccounts(int id);

        void AddLoan(LoanDTO loan);

        void AddNewCustomerProfile(RegisterCustomerDTO customerModel);
    }
}