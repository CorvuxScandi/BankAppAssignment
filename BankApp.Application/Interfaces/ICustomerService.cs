using BankApp.Application.ApiModels;
using BankApp.Domain.Models;

namespace BankApp.Application.Interfaces
{
    public interface ICustomerService
    {
        BankCustomerModel GetAccount();

        

        void Addtransaction(Transaction transaction);

    }
}
