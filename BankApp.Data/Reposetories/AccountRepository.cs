using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class AccountRepository : IRepository<Account>
    {
        public BankAppDataContext _context;

        public AccountRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void Create(Account entity)
        {
            _context.Add(entity);
        }

        public void Delete(Account entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts;
        }

        public Account GetById(int id)
        {
            return _context.Accounts.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Account entity)
        {
            _context.Update(entity);
        }
    }
}
