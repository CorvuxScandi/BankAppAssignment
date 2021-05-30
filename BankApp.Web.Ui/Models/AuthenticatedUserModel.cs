using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Ui.Models
{
    public class AuthenticatedUserModel
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
        public bool Authenticaded { get; set; }

    }
}
