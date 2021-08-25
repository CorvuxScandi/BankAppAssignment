using AutoMapper;
using BankApp.Application.Interfaces;
using BankApp.Application.Tools;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
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
            var customer = _customerRepo.GetAll().FirstOrDefault(x => x.CustomerId == id);

            var CustomerDispositions =
                _dispositionRepo
                .GetAll()
                .Where(x => x.CustomerId == id);

            CustomerInfoDTO customerInfo = new()
            {
                CustomerInfo = CustomMapper.MapDTO<Customer, CustomerDTO>(customer)
            };

            foreach (var item in CustomerDispositions)
            {
                //Mapps customer into API model
                customerInfo.Accounts
                    .Add(CustomMapper.MapDTO<Account, AccountDTO>(item.Account));

                //Mapps each card that the customer posesses into the API model
                foreach (var card in item.Cards)
                {
                    customerInfo.Cards.Add(CustomMapper.MapDTO<Card, CardDTO>(card));
                };

                //Mapps all loans the customer have into the API model
                foreach (var loan in item.Account.Loans)
                {
                    customerInfo.Loans.Add(CustomMapper.MapDTO<Loan, LoanDTO>(loan));
                }
            }

            return customerInfo;
        }

        public List<TransactionDTO> GetTransactions(int accountID)
        {
            var account = _accountRepo.GetById(accountID);

            List<TransactionDTO> transactions = new();

            foreach (var transfer in account.Transactions)
            {
                transactions.Add(CustomMapper.MapDTO<Transaction, TransactionDTO>(transfer));
            }

            return transactions;
        }
    }
}