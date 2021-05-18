using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Domain.DomainModels;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Services
{
    public class AdminServices : IAdminService
    {

        #region classStart
        private IRepository<Account> _accountRepo;
        private IRepository<AccountType> _TypeRepo;
        private IRepository<Card> _cardRepo;
        private IRepository<Customer> _customerRepo;
        private IRepository<Disposition> _dispositionRepo;
        private IRepository<Loan> _loanRepo;

        private  UserManager<ApplicationUser> _userManager;

        public AdminServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public AdminServices(IRepository<Account> accountRepo,
            IRepository<AccountType> typeRepo, IRepository<Card> cardRepo,
            IRepository<Customer> customerRepo, IRepository<Disposition> dispositionRepo,
            IRepository<Loan> loanRepo)
        {
            _accountRepo = accountRepo;
            _TypeRepo = typeRepo;
            _cardRepo = cardRepo;
            _customerRepo = customerRepo;
            _dispositionRepo = dispositionRepo;
            _loanRepo = loanRepo;
        }
        #endregion

        public ApplicationResponce AddAccountType(AccountType accountType)
        {
            _TypeRepo.Create(accountType);
            int changedElements = _TypeRepo.Save();
            if (changedElements > 0)
            {
                return new()
                {
                    ResponceCode = 200
                };
            }
            else
            {
                return new()
                {
                    ResponceCode = 400
                };
            }
        }

        public async Task<ApplicationResponce> AddNewCustomerProfile(BankCustomerModel model, RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByNameAsync(registerModel.Username);
            if (userExists != null)
                return new()
                {
                    ResponceCode = 409,
                    ResponceText = "User alredy exists"
                };

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.Username
            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded) return new() { ResponceCode = 400, ResponceText = "Server error creating new user" };

            model.AccountHolder.ApplicationUserId = user.Id;
            _customerRepo.Create(model.AccountHolder);
            if (_customerRepo.Save() < 0) return new() { ResponceCode = 500 };

            _accountRepo.Create(model.Accounts[0]);
            if (_customerRepo.Save() < 0) return new() { ResponceCode = 500 };

            _dispositionRepo.Create(new()
            {
                AccountId = model.Accounts[0].AccountId,
                CustomerId = model.AccountHolder.CustomerId
            });
            if (_dispositionRepo.Save() < 0) return new() { ResponceCode = 500 };

            return new()
            {
                ResponceCode = 200
            };
        }

        public ApplicationResponce FreezeAccount(BankCustomerModel account)
        {
            throw new NotImplementedException();
        }

        public ApplicationResponce FreezeCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public ApplicationResponce GetCustomerProfile(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationResponce> UpdateCustomerProfile(BankCustomerModel customer, RegisterModel registerModel)
        {
            throw new NotImplementedException();
        }
    }
}