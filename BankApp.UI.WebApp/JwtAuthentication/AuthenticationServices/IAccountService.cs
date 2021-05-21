using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.UI.WebApp.JwtAuthentication.AuthenticationServices
{
    public interface IAccountService
    {
        bool Login();
        bool Logout();
    }
}
