using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Domain.IdentityModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User name is requierd")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is requierd")]
        public string Password { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is requierd")]
        public string Email { get; set; }

    }
}
