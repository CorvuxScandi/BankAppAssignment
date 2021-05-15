using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Interfaces
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetLoans();

        Loan GetLoan(int id);

        void PostLoan(Loan loan);

        void PutLoan(Loan loan);

        void DeleteLoan(Loan loan);
    }
}
