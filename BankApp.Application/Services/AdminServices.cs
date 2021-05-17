using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Domain.DomainModels;
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
        private IAccountRepository _accountRepo;
        private IAccountTypeRepository _TypeRepo;
        private ICardRepository _cardRepo;
        private ICustomerRepository _customerRepo;
        private IDispositionRepository _dispositionRepo;
        private ILoanRepository _loanRepo;

        public AdminServices(IAccountRepository accountRepo,
            IAccountTypeRepository typeRepo, ICardRepository cardRepo,
            ICustomerRepository customerRepo, IDispositionRepository dispositionRepo,
            ILoanRepository loanRepo)
        {
            _accountRepo = accountRepo;
            _TypeRepo = typeRepo;
            _cardRepo = cardRepo;
            _customerRepo = customerRepo;
            _dispositionRepo = dispositionRepo;
            _loanRepo = loanRepo;
        }

        public ApplicationResponce AddAccountType(AccountType accountType)
        {
            _TypeRepo.PostType(accountType);
            return new()
            {
                ResponceCode = 200
            };
        }

        public ApplicationResponce AddNewCustomerProfile(BankCustomerModel model, IdentityUser identity)
        {
            _customerRepo.PostCustomer(model.AccountHolder);
            _accountRepo.PostAccount(model.Accounts[0]);
            _dispositionRepo.PostDisposition(new()
            {
                AccountId = model.Accounts[0].AccountId,
                CustomerId = model.AccountHolder.CustomerId
            });
            

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

        public ApplicationResponce GetCustomerProfile(BankCustomerModel customerModel)
        {
            throw new NotImplementedException();
        }

        public ApplicationResponce UpdateCustomerProfile(BankCustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
