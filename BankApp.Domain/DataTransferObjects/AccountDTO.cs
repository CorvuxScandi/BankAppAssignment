using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Enteties.DataTransferObjects
{
    public class AccountDTO
    {
        [DisplayName("Kund ID")]
        public int CustomerId { get; set; }

        [DisplayName("Konto ID")]
        public int AccountId { get; set; }

        public int AccountTypesId { get; set; }

        [DisplayName("Frekvens")]
        public string Frequency { get; set; }

        [DisplayName("Skapad")]
        public DateTime Created { get; set; }

        [DisplayName("Saldo")]
        public decimal Balance { get; set; }
    }
}