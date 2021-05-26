using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Application.Tools;
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

        public AdminServices(IRepository<Account> accountRepo,
            IRepository<AccountType> typeRepo, IRepository<Card> cardRepo,
            IRepository<Customer> customerRepo, IRepository<Disposition> dispositionRepo,
            IRepository<Loan> loanRepo, UserManager<ApplicationUser> userManager)
        {
            _accountRepo = accountRepo;
            _TypeRepo = typeRepo;
            _cardRepo = cardRepo;
            _customerRepo = customerRepo;
            _dispositionRepo = dispositionRepo;
            _loanRepo = loanRepo;
            _userManager = userManager;

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

        public ApplicationResponce AddLoan(Loan loan)
        {
            _loanRepo.Create(loan);
            var result = _loanRepo.Save();
            if (result > 0) return new() { ResponceCode = 200 };
            return new()
            {
                ResponceCode = 500,
                ResponceText = "Unknown server error"
            };
        }

        public async Task<ApplicationResponce> AddNewCustomerProfile(BankCustomerModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.RegisterModel.Username);
            if (userExists != null)
                return new()
                {
                    ResponceCode = 409,
                    ResponceText = "User alredy exists"
                };

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.RegisterModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.RegisterModel.Username
            };
            var result = await _userManager.CreateAsync(user, model.RegisterModel.Password);
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

        public List<AccountDTO> GetCustomerAccounts(int id)
        {
            var dispositions = _dispositionRepo.GetAll().Where(x => x.CustomerId == id);
            List<AccountDTO> accounts = new();
            foreach (var dis in dispositions)
            {
                accounts.Add(CustomMapper.MapDTO<Account, AccountDTO>(_accountRepo.GetById(dis.AccountId)));
            }
            return accounts;

        }

        public List<CustomerDTO> GetCostummers()
        {
            var result = _customerRepo.GetAll().ToList();
            List<CustomerDTO> list = new();
            foreach (var item in result)
            {
                list.Add(CustomMapper.MapDTO<Customer, CustomerDTO>(item));
            }
            return list;
        }

        public ApplicationResponce GetCustomerProfile(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationResponce> UpdateUserLogin(Customer costumer, RegisterModel registerModel)
        {
            var user = _userManager.FindByIdAsync(costumer.ApplicationUserId).Result;
            user.UserName = registerModel.Username;
            user.Email = registerModel.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return new() { ResponceCode = 200 };
            return new() { ResponceCode = 400, ResponceText="Unexpected server error" };
        }
    }
}