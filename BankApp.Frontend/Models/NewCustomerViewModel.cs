using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Frontend.Models
{
    public class NewCustomerViewModel
    {
        public CustomerDTO Customer { get; set; }

        public AccountDTO Account { get; set; }

        public RegristrationDTO Regristration { get; set; }

        public List<AccountTypeDTO> AccountTypes { get; set; }
    }
}