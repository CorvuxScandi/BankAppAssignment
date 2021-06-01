using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Models
{
    public class InternalTransaction
    {
        public int FromAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int ToAccount { get; set; }
    }
}