using BankApp.Application.Interfaces;
using BankApp.Application.Tools;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public CustomerService(IRepository<Account> accountRepo,
            IRepository<AccountType> typeRepo,
            IRepository<Card> cardRepo,
            IRepository<Customer> customerRepo,
            IRepository<Disposition> dispositionRepo,
            IRepository<Loan> loanRepo,
            IRepository<Transaction> transactionRepo)
        {
            _accountRepo = accountRepo;
            _TypeRepo = typeRepo;
            _cardRepo = cardRepo;
            _customerRepo = customerRepo;
            _dispositionRepo = dispositionRepo;
            _loanRepo = loanRepo;
            _transactionRepo = transactionRepo;
        }

        public void Addtransaction(Transaction transaction)
        {
            Account fromAccount = _accountRepo.GetById(transaction.AccountId);
            if (transaction.Account is not null)
            {
                var toAccount = _accountRepo.GetById(Int32.Parse(transaction.Account));
                if (toAccount != null)
                {
                    Transaction transactionTo = new()
                    {
                        AccountId = toAccount.AccountId,
                        Account = transaction.AccountId.ToString(),
                        Amount = Math.Abs(transaction.Amount),
                        Balance = toAccount.Balance += Math.Abs(transaction.Amount),
                        Bank = transaction.Bank,
                        Date = transaction.Date,
                        Operation = transaction.Operation,
                        Symbol = transaction.Symbol,
                        Type = transaction.Type
                    };
                    toAccount.Balance += transaction.Amount;
                    _transactionRepo.Create(transactionTo);
                    _accountRepo.Update(toAccount);
                }
            }
            fromAccount.Balance -= transaction.Amount;
            transaction.Balance = fromAccount.Balance;

            _transactionRepo.Create(transaction);
            _accountRepo.Update(fromAccount);

            _accountRepo.Save();
            _transactionRepo.Save();
        }

        public CustomerInfoDTO GetCustomerInfo(int id)
        {
            CustomerDTO customer = CustomMapper.MapDTO<Customer, CustomerDTO>(_customerRepo.GetById(id));
            var customerDispositions = _dispositionRepo.GetAll().ToList().Where(d => d.CustomerId == id).ToList();
            List<AccountDTO> accounts = new();
            List<CardDTO> cards = new();
            List<LoanDTO> loans = new();

            foreach (var disposition in customerDispositions)
            {
                //Maps customer into API model
                var account = CustomMapper.MapDTO<Account, AccountDTO>(_accountRepo.GetById(disposition.AccountId));
                if (account != null) accounts.Add(account);
                var card = CustomMapper.MapDTO<Card, CardDTO>(_cardRepo.GetAll().ToList().FirstOrDefault(c => c.DispositionId == disposition.DispositionId));
                if (card != null) cards.Add(card);

                foreach (var item in _loanRepo.GetAll().Where(l => l.AccountId == account.AccountId).ToList())
                {
                    loans.Add(CustomMapper.MapDTO<Loan, LoanDTO>(item));
                }
            }

            return new()
            {
                CustomerInfo = customer,
                Accounts = accounts,
                Cards = cards,
                Loans = loans
            };
        }

        public PagedList<TransactionDTO> GetTransactions(TransactionParameters parameters)
        {
            List<TransactionDTO> transactionsDTO = new();

            var transactions = _transactionRepo.GetAll().Where(t => t.AccountId == parameters.AccountId).ToList();
            var transSkipped = transactions
                .OrderByDescending(t => t.Date)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToList();

            foreach (var transfer in transactions)
            {
                transactionsDTO.Add(CustomMapper.MapDTO<Transaction, TransactionDTO>(transfer));
            }

            return new PagedList<TransactionDTO>(transactionsDTO, transactions.Count, parameters.PageNumber, parameters.PageSize);
        }
    }
}