using BankApp.Data.Reposetories;
using BankApp.Domain.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data
{
    public static class InitiateAccounts
    {
        public static async void Initiate(UserManager<ApplicationUser> _manager, CustomerRepository _customerRepository)
        {
            var admin = await _manager.FindByEmailAsync("admin@mail.com");
            if (admin == null)
            {
                ApplicationUser newAdmin = new()
                {
                    Email = "admin@mail.com"
                };

                await _manager.CreateAsync(newAdmin, "!Sunshine1");

                var cust = _customerRepository.GetById(8);

                ApplicationUser user = new()
                {
                    Email = cust.Emailaddress
                };
                await _manager.CreateAsync(user, "!Sunshine1");

                cust.ApplicationUserId = user.Id;
                _customerRepository.Update(cust);
            }
        }
    }
}