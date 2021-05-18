using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Domain.DomainModels;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
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

            _accountRepo.Update(account);

            return new()
            {
                ResponceCode = 202,
                ResponceText = "Succsess"
            };
        }

        public ApplicationResponce FindCustomerIdentity(string id)
        {
            var customer = _customerRepo.GetAll().FirstOrDefault(c => c.ApplicationUserId == id);
            if (customer == null)
            {
                return new()
                {
                    ResponceCode = 404,
                    ResponceText = "Customer not found"
                };
            }
            return new() { ResponceCode = 200, ResponceBody = customer };
        }

        public ApplicationResponce GetAccountInfo(Customer customer)
        {
            
            var CustomerDispositions =
                _dispositionRepo
                .GetAll()
                .Where(x => x.CustomerId == customer.CustomerId);

            BankCustomerModel bankCustomer = new();

            bankCustomer.AccountHolder = customer;

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
            if (_accountRepo.GetById(accountId) == null)
            {
                return new()
                {
                    ResponceCode = 404,
                    ResponceText = "Account not found"
                };
            };

            return new()
            {
                ResponceCode = 200,
                ResponceBody = _accountRepo.GetById(accountId).Transactions.ToList()
            };
        }
    }
}