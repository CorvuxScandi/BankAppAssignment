using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Web.Ui.Models
{
    public class RegisterModel
    {
        
        public CustomerDTO Customer { get; set; }

        public AccountDTO Account { get; set; }
        
    }
}