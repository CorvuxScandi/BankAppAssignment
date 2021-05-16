using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Application.ApiModels
{
    public class BankCustomerModel
    {
        public Customer AccountHolder { get; set; }

        public List<Account> Accounts { get; set; }

        public List<Card> ConnectedCards { get; set; }

        public List<Loan> Loans { get; set; }

        public List<Transaction> AccountTransactions { get; set; }

    }
}
