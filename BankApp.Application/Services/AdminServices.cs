using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Domain.DomainModels;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Services
{
    public class AdminServices : IAdminService
    {
        public IAccountRepository _accountRepo;
        public IAccountTypeRepository _TypeRepo;
        public ICardRepository _cardRepo;
        public ICustomerRepository _customerRepo;
        public IDispositionRepository _dispositionRepo;
        public ILoanRepository _loanRepo;

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
            throw new NotImplementedException();
        }

        public ApplicationResponce AddNewCustomerProfile(BankCustomerModel model)
        {
            throw new NotImplementedException();
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
