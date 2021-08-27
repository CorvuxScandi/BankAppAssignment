using BankApp.Application.Interfaces;
using BankApp.Application.Tools;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
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

        public void Addtransaction(InternalTransaction transaction)
        {
            Account fromAccount = _accountRepo.GetById(transaction.FromAccount);
            Account toAccount = _accountRepo.GetById(transaction.ToAccount);

            Transaction transFrom = new()
            {
                Account = "internal: " + transaction.ToAccount.ToString(),
                AccountId = transaction.FromAccount,
                Amount = transaction.Amount,
                Balance = fromAccount.Balance + transaction.Amount,
                Date = transaction.Date,
                Type = "Internal bank transaction"
            };

            Transaction transTo = new()
            {
                Account = "internal: " + transaction.FromAccount.ToString(),
                AccountId = transaction.ToAccount,
                Amount = transaction.Amount,
                Balance = toAccount.Balance + System.Math.Abs(transaction.Amount),
                Date = transaction.Date,
                Type = "Internal bank transaction"
            };

            fromAccount.Balance = transFrom.Balance;
            toAccount.Balance = transTo.Balance;

            _transactionRepo.Create(transFrom);
            _transactionRepo.Create(transTo);
            _transactionRepo.Save();

            _accountRepo.Update(fromAccount);
            _accountRepo.Update(toAccount);
            _accountRepo.Save();
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

            var transactions = _transactionRepo.GetAll().ToList().Where(t => t.AccountId == parameters.AccountId)
                .OrderByDescending(t => t.Date)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToList();

            foreach (var transfer in transactions)
            {
                transactionsDTO.Add(CustomMapper.MapDTO<Transaction, TransactionDTO>(transfer));
            }

            return new PagedList<TransactionDTO>(transactionsDTO, transactionsDTO.Count, parameters.PageNumber, parameters.PageSize);
        }
    }
}