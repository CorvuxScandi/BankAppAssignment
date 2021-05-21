using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.JwtAuthentication.AuthenticationServices
{
    public class AccountService : IAccountService
    {
        private readonly AuthenticationStateProvider _stateProvider;

        public AccountService(AuthenticationStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
        }

        public bool Login()
        {
            (_stateProvider as CustomAuthenticationProvider).LoginNotify();
            return true;
        }

        public bool Logout()
        {
            (_stateProvider as CustomAuthenticationProvider).LogoutNotify();
            return true;

        }
    }
}
