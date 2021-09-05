using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;

namespace BankApp.Application.Interfaces
{
    public interface ICustomerService
    {
        CustomerInfoDTO GetCustomerInfo(int id);

        void Addtransaction(Transaction transaction);

        PagedList<TransactionDTO> GetTransactions(TransactionParameters parameters);
    }
}