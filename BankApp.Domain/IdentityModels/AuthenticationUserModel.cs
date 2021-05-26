using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Api.Models
{
    public class AuthenticationUserModel
    {
        [Required(ErrorMessage = "Missing user name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Missing password")]
        public string Password { get; set; }
    }
}