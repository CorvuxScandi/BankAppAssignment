using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Ui.Models
{
    public class AuthenticationUserModel
    {
        [Required(ErrorMessage = "Missing email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Missing password")]
        public string Password { get; set; }
    }
}