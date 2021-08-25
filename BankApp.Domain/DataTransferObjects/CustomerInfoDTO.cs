using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Enteties.DataTransferObjects
{
    public class CustomerInfoDTO
    {
        public CustomerDTO CustomerInfo { get; set; }
        public List<AccountDTO> Accounts { get; set; }
        public List<CardDTO> Cards { get; set; }
        public List<LoanDTO> Loans { get; set; }
    }
}