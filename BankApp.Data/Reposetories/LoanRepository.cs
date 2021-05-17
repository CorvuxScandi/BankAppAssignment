using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class LoanRepository : IRepository<Loan>
    {
        public BankAppDataContext _context;

        public LoanRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void Create(Loan entity)
        {
            _context.Add(entity);
        }

        public void Delete(Loan entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<Loan> GetAll()
        {
            return _context.Loans;
        }

        public Loan GetById(int id)
        {
            return _context.Loans.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Loan entity)
        {
            _context.Update(entity);
        }
    }
}