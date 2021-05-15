using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        public BankAppDataContext _context;

        public AccountTypeRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void DeleteType(AccountType accountType)
        {
            _context.AccountTypes.Remove(accountType);
            _context.SaveChanges();
        }

        public AccountType GetType(int id)
        {
            return _context.AccountTypes.Find(id);
        }

        public IEnumerable<AccountType> GetTypes()
        {
            return _context.AccountTypes;
        }

        public void PostType(AccountType accountType)
        {
            _context.Add(accountType);
            _context.SaveChanges();
        }

        public void PutType(AccountType accountType)
        {
            _context.Update(accountType);
            _context.SaveChanges();
        }
    }
}
