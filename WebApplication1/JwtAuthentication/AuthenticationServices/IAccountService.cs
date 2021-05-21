using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.JwtAuthentication.AuthenticationServices
{
    public interface IAccountService
    {
        bool Login();
        bool Logout();
    }
}
