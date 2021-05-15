using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class AccountRepository : IAccountRepository
    {
        public BankAppDataContext _context;

        public AccountRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }

        public Account GetAccount(int id)
        {
            return _context.Accounts.Find(id);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts;
        }

        public void PostAccount(Account account)
        {
            _context.Add(account);
            _context.SaveChanges();
        }

        public void PutAccount(Account account)
        {
            _context.Update(account);
            _context.SaveChanges();
        }
    }
}
