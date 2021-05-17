using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        public BankAppDataContext _context;

        public TransactionRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void Create(Transaction entity)
        {
            _context.Add(entity);
        }

        public void Delete(Transaction entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions;
        }

        public Transaction GetById(int id)
        {
            return _context.Transactions.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Transaction entity)
        {
            _context.Update(entity);
        }
    }
}