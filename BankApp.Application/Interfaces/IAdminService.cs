using BankApp.Application.ApiModels;
using BankApp.Domain.Models;

namespace BankApp.Application.Interfaces
{
    public interface IAdminService
    {
        void AddDisposition(Disposition disposition);

        void PostNewAccount(BankCustomerModel newAccount);

        void DeleteAccount(BankCustomerModel account);

        void AddLoan(Loan newLoan);
    }
}
