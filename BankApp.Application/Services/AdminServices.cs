using BankApp.Application.Interfaces;
using BankApp.Application.Tools;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using BankApp.Enteties.Models.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Application.Services
{
    public class AdminServices : IAdminService
    {
        #region classStart

        private IRepository<Account> _accountRepo;
        private IRepository<AccountType> _TypeRepo;
        private IRepository<Customer> _customerRepo;
        private IRepository<Disposition> _dispositionRepo;
        private IRepository<Loan> _loanRepo;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IRepository<Transaction> _transaction;

        public AdminServices(IRepository<Account> accountRepo,
            IRepository<AccountType> typeRepo, IRepository<Card> cardRepo,
            IRepository<Customer> customerRepo, IRepository<Disposition> dispositionRepo,
            IRepository<Loan> loanRepo, UserManager<ApplicationUser> userManager, IRepository<Transaction> transaction, RoleManager<IdentityRole> roleManager)
        {
            _accountRepo = accountRepo;
            _TypeRepo = typeRepo;
            _customerRepo = customerRepo;
            _dispositionRepo = dispositionRepo;
            _loanRepo = loanRepo;
            _userManager = userManager;
            _transaction = transaction;
            _roleManager = roleManager;
        }

        #endregion classStart

        public void AddAccountType(AccountType accountType)
        {
            _TypeRepo.Create(accountType);
            _TypeRepo.Save();
        }

        public void AddLoan(Loan loan)
        {
            var account = _accountRepo.GetById(loan.AccountId);
            account.Balance += loan.Amount;
            _accountRepo.Update(account);
            _loanRepo.Create(loan);

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
            _loanRepo.Save();
        }

        public void AddNewCustomerProfile(RegisterCustomerDTO customerModel)
        {
            Customer newC = CustomMapper.MapDTO<CustomerDTO, Customer>(customerModel.Customer);
            Account newA = new()
            {
                Created = DateTime.Today,
                Balance = 0,
                AccountTypesId = customerModel.Account.AccountTypesId,
                Frequency = customerModel.Account.Frequency,
            };

            _customerRepo.Create(newC);
            _accountRepo.Create(newA);

            Disposition newDispo = new()
            {
                CustomerId = newC.CustomerId,
                AccountId = newA.AccountId
            };

            //Repo create and save

            _dispositionRepo.Create(newDispo);

            _dispositionRepo.Save();
            _customerRepo.Save();
            _accountRepo.Save();
        }

        public List<AccountDTO> GetCustomerAccounts(int id)
        {
            var dispositions = _dispositionRepo.GetAll().Where(x => x.CustomerId == id);

            List<AccountDTO> accounts = new();

            foreach (var dis in dispositions)
            {
                var account = _accountRepo.GetById(dis.AccountId);
                accounts.Add(CustomMapper.MapDTO<Account, AccountDTO>(account));
            }

            return accounts;
        }

        public PagedList<CustomerDTO> GetCustomers(CustomerParameters parameters)
        {
            var customers = _customerRepo.GetAll()
                .OrderBy(c => c.Surname)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();

            List<CustomerDTO> customersDTO = new();

            foreach (var customer in customers)
            {
                customersDTO.Add(CustomMapper.MapDTO<Customer, CustomerDTO>(customer));
            }

            return new PagedList<CustomerDTO>(customersDTO, customersDTO.Count, parameters.PageNumber, parameters.PageSize);
        }

        public List<AccountTypeDTO> GetAccountTypes()
        {
            var types = _TypeRepo.GetAll().ToList();

            List<AccountTypeDTO> typesDTO = new();

            foreach (var type in types)
            {
                typesDTO.Add(CustomMapper.MapDTO<AccountType, AccountTypeDTO>(type));
            }

            return typesDTO;
        }

        public void AddCustomerLogin(RegristrationDTO regristration)
        {
            var user = _userManager.FindByNameAsync(regristration.Email).Result;

            if (user is not null)
            {
                ApplicationUser newUser = new()
                {
                    Email = regristration.Email,
                    UserName = regristration.Email,
                };
                _userManager.CreateAsync(newUser, regristration.Password);
                _userManager.AddToRolesAsync(newUser, regristration.Roles.ToList());
            }
        }
    }
}