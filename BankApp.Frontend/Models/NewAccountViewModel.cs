using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Frontend.Models
{
    public class NewAccountViewModel
    {
        public AccountDTO Account { get; set; }

        public List<AccountTypeDTO> AccountTypes { get; set; }
    }
}