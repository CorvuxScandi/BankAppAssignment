using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Enteties.DataTransferObjects
{
    public class RegisterCustomerDTO
    {
        public CustomerDTO Customer { get; set; }

        public AccountDTO Account { get; set; }
    }
}