using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.Reposetories
{
    internal class InternalTransactionRepository : IRepository<InternalTransaction>
    {
        private BankAppDataContext _context;

        public InternalTransactionRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void Create(InternalTransaction entity)
        {
            _context.Add(entity);
        }

        public void Delete(InternalTransaction entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<InternalTransaction> GetAll()
        {
            return _context.InternalTransactions;
        }

        public InternalTransaction GetById(int id)
        {
            return _context.InternalTransactions.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(InternalTransaction entity)
        {
            _context.Update(entity);
        }
    }
}