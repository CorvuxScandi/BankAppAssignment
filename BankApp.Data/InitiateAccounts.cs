using BankApp.Data.Contexts;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Data
{
    public static class InitiateAccounts
    {
        public static void SeedData(UserManager<ApplicationUser> _manager, RoleManager<IdentityRole> _roleManager, BankAppDataContext appDataContext)
        {
            SeedRoles(_roleManager);
            SeedAdmin(_manager);
            SeedUsers(_manager, appDataContext);
        }

        public static void SeedRoles(RoleManager<IdentityRole> _roleManager)
        {
            if (!_roleManager.RoleExistsAsync(UserRoles.Admin).Result)
            {
                _ = _roleManager.CreateAsync(new()
                {
                    Name = UserRoles.Admin
                });
            }
            if (!_roleManager.RoleExistsAsync(UserRoles.User).Result)
            {
                _ = _roleManager.CreateAsync(new()
                {
                    Name = UserRoles.User
                });
            }
        }

        public static void SeedAdmin(UserManager<ApplicationUser> _manager)
        {
            if (_manager.FindByNameAsync("admin@mail.com").Result == null)
            {
                ApplicationUser admin = new()
                {
                    Email = "admin@mail.com",
                    UserName = "admin@mail.com"
                };
                IdentityResult result = _manager.CreateAsync(admin, "!Sunshine1").Result;
                if (result.Succeeded)
                {
                    _manager.AddToRoleAsync(admin, UserRoles.Admin).Wait();
                }
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> _manager, BankAppDataContext _bankAppDataContext)
        {
            var customers = _bankAppDataContext.Customers.ToList();
            foreach (var cust in customers)
            {
                var email = cust.Emailaddress;

                if (_manager.FindByNameAsync(email).Result is null)
                {
                    ApplicationUser user = new()
                    {
                        Email = email,
                        UserName = email
                    };

                    IdentityResult result = _manager.CreateAsync(user, "!Sunshine1").Result;
                    if (result.Succeeded)
                    {
                        _manager.AddToRoleAsync(user, UserRoles.User).Wait();
                    }
                }
            }
        }
    }
}