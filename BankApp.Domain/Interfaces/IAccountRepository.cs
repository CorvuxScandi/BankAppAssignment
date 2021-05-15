using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();

        void DeleteAccount(Account account);

        Account GetAccount(int id);
    }
}
