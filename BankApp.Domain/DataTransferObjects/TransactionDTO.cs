using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Enteties.DataTransferObjects
{
    public class TransactionDTO
    {
        [DisplayName("Id")]
        public int AccountId { get; set; }

        [DisplayName("Datum")]
        public DateTime Date { get; set; }

        [DisplayName("Transaktions typ")]
        public string Type { get; set; }

        [DisplayName("Funktion")]
        public string Operation { get; set; }

        [DisplayName("Belopp")]
        public decimal Amount { get; set; }

        [DisplayName("Saldo")]
        public decimal Balance { get; set; }

        [DisplayName("Bank")]
        public string Bank { get; set; }

        [DisplayName("Konto")]
        public string Account { get; set; }
    }
}