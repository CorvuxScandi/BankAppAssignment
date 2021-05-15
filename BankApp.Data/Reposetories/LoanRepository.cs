using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class LoanRepository : ILoanRepository
    {
        public BankAppDataContext _context;

        public LoanRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void DeleteLoan(Loan loan)
        {
            _context.Loans.Remove(loan);
            _context.SaveChanges();
        }

        public Loan GetLoan(int id)
        {
            return _context.Loans.Find(id);
        }

        public IEnumerable<Loan> GetLoans()
        {
            return _context.Loans;
        }

        public void PostLoan(Loan loan)
        {
            _context.Add(loan);
            _context.SaveChanges();
        }

        public void PutLoan(Loan loan)
        {
            _context.Update(loan);
            _context.SaveChanges();
        }
    }
}
