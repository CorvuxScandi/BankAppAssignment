using BankApp.Enteties.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Frontend.Models
{
    public class GiveLoanViewModel
    {
        public CustomerInfoDTO CustomerInfo { get; set; }

        public LoanDTO LoanDTO { get; set; }

    }
}
