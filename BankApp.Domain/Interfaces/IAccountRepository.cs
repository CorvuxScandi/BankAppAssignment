using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
        
        Account GetAccount(int id);

        void PostAccount(Account account);

        void PutAccount(Account account);

        void DeleteAccount(Account account);

        
    }
}
