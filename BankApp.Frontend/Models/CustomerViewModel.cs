using BankApp.Enteties.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Frontend.Models
{
    public class CustomerViewModel
    {
        public CustomerInfoDTO CustomerInfo { get; set; }

        public List<AccountTypeDTO> AccountTypes { get; set; }

    }
}
