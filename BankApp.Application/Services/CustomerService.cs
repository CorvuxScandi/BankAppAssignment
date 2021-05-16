using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Domain.DomainModels;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace BankApp.Application.Services
{
    public class AccountService : ICustomerService
    {
        public IAccountRepository _accountRepo;
        public IAccountTypeRepository _TypeRepo;
        public ICardRepository _cardRepo;
        public ICustomerRepository _customerRepo;
        public IDispositionRepository _dispositionRepo;
        public ILoanRepository _loanRepo;
        public ITransactionRepository _transactionRepo;

        public AccountService(
            IAccountRepository accountRepo, 
            IAccountTypeRepository typeRepo, 
            ICardRepository cardRepo, 
            ICustomerRepository customerRepo, 
            IDispositionRepository dispositionRepo, 
            ILoanRepository loanRepo, 
            ITransactionRepository transactionRepo)
        {
            _accountRepo = accountRepo;
            _TypeRepo = typeRepo;
            _cardRepo = cardRepo;
            _customerRepo = customerRepo;
            _dispositionRepo = dispositionRepo;
            _loanRepo = loanRepo;
            _transactionRepo = transactionRepo;
        }

        public ApplicationResponce Addtransaction(Transaction transaction)
        {
            Account account = transaction.AccountNavigation;
            if (account.Balance < transaction.Amount)
            {

                return new() 
                {
                    ResponceCode = 409, 
                    ResponceText = "Account balance too low" 
                };
            }

            account.Balance -= transaction.Amount;

            _accountRepo.PutAccount(account);

            return new() 
            {
                ResponceCode = 202, 
                ResponceText = "Succsess" 
            };
        }

        public ApplicationResponce GetAccountInfo(int customerId)
        {
            if (_customerRepo.GetCustomer(customerId) == null)
            {
                return new()
                {
                    ResponceCode = 404,
                    ResponceText = "Customer not found"
                };
            }
                
            
            var CustomerDispositions = 
                _dispositionRepo
                .GetDispositions()
                .Where(x => x.CustomerId == customerId);

            BankCustomerModel bankCustomer = new();

            bankCustomer.AccountHolder = 
                _customerRepo
                .GetCustomer(customerId);
            
            foreach (var item in CustomerDispositions)
            {
                bankCustomer.Accounts
                    .Add(item.Account);
                bankCustomer.ConnectedCards
                    .AddRange(item.Cards);
                bankCustomer.Loans
                    .AddRange(item.Account.Loans);
            }

            return new() 
            {
                ResponceCode = 200, 
                ResponceBody = bankCustomer
            };
        }

        public ApplicationResponce GetTransactions(int accountId)
        {
            if (_accountRepo.GetAccount(accountId) == null)
            {
                return new()
                {
                    ResponceCode = 404,
                    ResponceText = "Account not found"
                };
            };


            return new() { 
                ResponceCode = 200, 
                ResponceBody = _accountRepo.GetAccount(accountId).Transactions.ToList() 
            };
        }
    }
}
