using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.Application.Interfaces
{
    public interface ICustomerService
    {
        CustomerInfoDTO GetCustomerInfo(int id);

        void Addtransaction(InternalTransaction transaction);

        List<TransactionDTO> GetTransactions(int accountId);
    }
}