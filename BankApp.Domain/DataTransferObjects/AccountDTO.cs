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