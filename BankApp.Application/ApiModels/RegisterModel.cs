using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.ApiModels
{
    public class RegisterModel
    {
        
        public CustomerDTO Customer { get; init; }

        public AccountDTO Account { get; init; }
        
    }
}