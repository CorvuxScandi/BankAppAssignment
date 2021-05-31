using AutoMapper;
using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Application.Tools;
using BankApp.Domain.DomainModels;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        public IRepository<Account> _accountRepo;
        public IRepository<AccountType> _TypeRepo;
        public IRepository<Card> _cardRepo;
        public IRepository<Customer> _customerRepo;
        public IRepository<Disposition> _dispositionRepo;
        public IRepository<Loan> _loanRepo;
        public IRepository<Transaction> _transactionRepo;
        public IRepository<InternalTransaction> _internalTransactionRepo;

        public CustomerService(IRepository<Account> accountRepo,
            IRepository<AccountType> typeRepo,
            IRepository<Card> cardRepo,
            IRepository<Customer> customerRepo,
            IRepository<Disposition> dispositionRepo,
            IRepository<Loan> loanRepo,
            IRepository<Transaction> transactionRepo,
            IRepository<InternalTransaction> internalTransactionRepo)
        {
            _accountRepo = accountRepo;
            _TypeRepo = typeRepo;
            _cardRepo = cardRepo;
            _customerRepo = customerRepo;
            _dispositionRepo = dispositionRepo;
            _loanRepo = loanRepo;
            _transactionRepo = transactionRepo;
            _internalTransactionRepo = internalTransactionRepo;
        }

        public ApplicationResponce Addtransaction(InternalTransaction transaction)
        {
            Transaction tran = new()
            {
                Account = "internal: " + transaction.ToAccount.ToString(),
                AccountId = transaction.FromAccount,
                Amount = transaction.Amount,
                Balance = transaction.Balance,
                Date = transaction.Date,
                Type = "Internal bank transaction"
            };

            Account account = _accountRepo.GetById(transaction.FromAccount);

            account.Balance -= transaction.Amount;

            _accountRepo.Update(account);
            _accountRepo.Save();

            _transactionRepo.Create(tran);
            _transactionRepo.Save();

            _internalTransactionRepo.Create(transaction);
            _internalTransactionRepo.Save();

            return new()
            {
                ResponceCode = 202,
                ResponceText = "Succsess"
            };
        }

        public ApplicationResponce GetCustomerInfo(string email)
        {
            var customer = _customerRepo.GetAll().FirstOrDefault(x => x.Emailaddress == email);

            var CustomerDispositions =
                _dispositionRepo
                .GetAll()
                .Where(x => x.CustomerId == customer.CustomerId);

            CustomerDetails bankCustomer = new()
            {
                CustomerInfo = CustomMapper.MapDTO<Customer, CustomerDTO>(customer)
            };

            foreach (var item in CustomerDispositions)
            {
                //Mapps customer into API model
                bankCustomer.Accounts
                    .Add(CustomMapper.MapDTO<Account, AccountDTO>(item.Account));

                //Mapps each card that the customer posesses into the API model
                foreach (var card in item.Cards)
                {
                    bankCustomer.Cards.Add(CustomMapper.MapDTO<Card, CardDTO>(card));
                };

                //Mapps all loans the customer have into the API model
                foreach (var loan in item.Account.Loans)
                {
                    bankCustomer.Loans.Add(CustomMapper.MapDTO<Loan, LoanDTO>(loan));
                }
            }

            return new()
            {
                ResponceCode = 200,
                ResponceBody = bankCustomer
            };
        }

        public ApplicationResponce GetTransactions(int accountID)
        {
            var account = _accountRepo.GetById(accountID);
            if (account is null)
            {
                return new()
                {
                    ResponceCode = 404,
                    ResponceText = "Account not found"
                };
            };

            List<TransferDTO> transactions = new();

            foreach (var transfer in account.Transactions)
            {
                transactions.Add(CustomMapper.MapDTO<Transaction, TransferDTO>(transfer));
            }

            return new()
            {
                ResponceCode = 200,
                ResponceBody = transactions
            };
        }
    }
}