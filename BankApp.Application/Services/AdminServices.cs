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
        private UserManager<ApplicationUser> _userManager;
        private IRepository<Transaction> _transaction;

        public AdminServices(IRepository<Account> accountRepo,
            IRepository<AccountType> typeRepo, IRepository<Card> cardRepo,
            IRepository<Customer> customerRepo, IRepository<Disposition> dispositionRepo,
            IRepository<Loan> loanRepo, UserManager<ApplicationUser> userManager, IRepository<Transaction> transaction)
        {
            _accountRepo = accountRepo;
            _TypeRepo = typeRepo;
            _cardRepo = cardRepo;
            _customerRepo = customerRepo;
            _dispositionRepo = dispositionRepo;
            _loanRepo = loanRepo;
            _userManager = userManager;
            _transaction = transaction;
        }

        #endregion classStart

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

        public ApplicationResponce AddLoan(LoanDTO loan)
        {
            var newLoan = CustomMapper.ReveceMap<LoanDTO, Loan>(loan);
            var account = _accountRepo.GetById(loan.AccountId);
            account.Balance += loan.Amount;
            _accountRepo.Update(account);
            _loanRepo.Create(newLoan);

            Transaction t = new()
            {
                AccountId = loan.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Credit in Cash",
                Amount = loan.Amount,
                Balance = (account.Balance += loan.Amount),
            };
            _transaction.Create(t);

            _accountRepo.Save();
            _transaction.Save();
            var result = _loanRepo.Save();
            if (result > 0) return new() { ResponceCode = 200 };
            return new()
            {
                ResponceCode = 500,
                ResponceText = "Unknown server error"
            };
        }

        public async Task<ApplicationResponce> AddNewCustomerProfile(RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Emailaddress);
            if (userExists != null)
                return new()
                {
                    ResponceCode = 409,
                    ResponceText = "User alredy exists"
                };
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Emailaddress,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await _userManager.CreateAsync(user, "!Sunshine1");
            if (!result.Succeeded) return new() { ResponceCode = 400, ResponceText = "Server error creating new user" };

            Customer newC = CustomMapper.MapDTO<RegisterModel, Customer>(model);
            newC.ApplicationUserId = user.Id;
            Account newA = CustomMapper.MapDTO<RegisterModel, Account>(model);
            newA.Created = DateTime.Today;

            //Repo create and save
            _customerRepo.Create(newC);
            _accountRepo.Create(newA);
            _customerRepo.Save();
            _accountRepo.Save();

            _dispositionRepo.Create(new()
            {
                AccountId = newA.AccountId,
                CustomerId = newC.CustomerId
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
                var account = CustomMapper.MapDTO<Account, AccountDTO>(_accountRepo.GetById(dis.AccountId));
                account.AccountType = dis.Account.AccountTypes.TypeName;
                accounts.Add(account);
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

        public Task<ApplicationResponce> UpdateUserLogin(RegisterModel registerModel)
        {
            throw new NotImplementedException();
        }
    }
}