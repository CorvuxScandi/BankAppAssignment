using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetTransactions();

        Transaction GetTransaction(int id);

        void PostTransaction(Transaction transaction);

        void PutTransaction(Transaction transaction);

        void DeleteTransaction(Transaction transaction);
    }
}
