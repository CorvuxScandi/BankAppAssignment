using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class AccountTypeRepository : IRepository<AccountType>
    {
        public BankAppDataContext _context;

        public AccountTypeRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void Create(AccountType entity)
        {
            _context.Add(entity);
        }

        public void Delete(AccountType entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<AccountType> GetAll()
        {
            return _context.AccountTypes;
        }

        public AccountType GetById(int id)
        {
            return _context.AccountTypes.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(AccountType entity)
        {
            _context.Update(entity);
        }
    }
}
