using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Application.ApiModels
{
    public class BankCustomerModel
    {
        public Account GeneralInfo { get; set; }

        public AccountType Type { get; set; }

        public Customer AccountHolder { get; set; }

        public IEnumerable<Card> ConnectedCards { get; set; }

        public IEnumerable<Loan> Loans { get; set; }

        public IEnumerable<Transaction> AccountTransactions { get; set; }

    }
}
