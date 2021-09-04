using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Frontend.Models
{
    public class CustomerPageModel
    {
        public PagedList<TransactionDTO> Transactions { get; set; }
        public List<AccountTypeDTO> AccountTypes { get; set; }
        public CustomerDTO Customer { get; set; }
        public List<AccountDTO> Accounts { get; set; }
        public List<CardDTO> Cards { get; set; }
        public List<LoanDTO> Loans { get; set; }
    }
}