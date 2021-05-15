using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class TransactionRepository : ITransactionRepository
    {
        public BankAppDataContext _context;

        public TransactionRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void DeleteTransaction(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }

        public Transaction GetTransaction(int id)
        {
            return _context.Transactions.Find(id);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _context.Transactions;
        }

        public void PostTransaction(Transaction transaction)
        {
            _context.Add(transaction);
            _context.SaveChanges();
        }

        public void PutTransaction(Transaction transaction)
        {
            _context.Update(transaction);
            _context.SaveChanges();
        }
    }
}
