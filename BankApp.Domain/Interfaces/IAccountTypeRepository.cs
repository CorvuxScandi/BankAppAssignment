using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Interfaces
{
    public interface IAccountTypeRepository
    {
        AccountType GetType(int id);

        void PostType(AccountType accountType);

        void DeleteType(AccountType accountType);
    }
}
