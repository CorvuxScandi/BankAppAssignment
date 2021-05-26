using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Api.Models
{
    public class AuthenticatedUserModel
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
    }
}