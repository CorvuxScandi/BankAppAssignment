using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Ui.Models
{
    public class InternalTransaction
    {
        public int FromAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int ToAccount { get; set; }
    }
}