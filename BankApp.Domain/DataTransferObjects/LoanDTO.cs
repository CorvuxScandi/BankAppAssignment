using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Enteties.DataTransferObjects
{
    public class LoanDTO
    {
        public int AccountId { get; set; }

        [DisplayName("Datum")]
        public DateTime Date { get; set; }

        [DisplayName("Mängd")]
        public decimal Amount { get; set; }

        [DisplayName("Bestånd")]
        public int Duration { get; set; }

        [DisplayName("Betalningar")]
        public decimal Payments { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
    }
}