using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Ui.Models
{
    public class AuthenticatedUserModel
    {
        
        
            public bool IsAuthSuccessful { get; set; }
            public string ErrorMessage { get; set; }
            public string Token { get; set; }
        
    }
}
