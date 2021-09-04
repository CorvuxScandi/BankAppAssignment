using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using System.Collections.Generic;

namespace BankApp.Frontend.Models
{
    public class CustomerViewModel
    {
        public CustomerInfoDTO CustomerInfo { get; set; }

        public List<AccountTypeDTO> AccountTypes { get; set; }

        public PagedList<TransactionDTO> PagedListTransactions { get; set; }
    }
}