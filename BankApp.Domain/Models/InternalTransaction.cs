using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Models
{
    public partial class InternalTransaction
    {
        public int InternalTransactionId { get; set; }
        public int FromAccount { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }
        public int ToAccount { get; set; }

        public virtual Account AccountNavigation { get; set; }
    }
}