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
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Account> accountRepo,
            IRepository<AccountType> typeRepo,
            IRepository<Card> cardRepo,
            IRepository<Customer> customerRepo,
            IRepository<Disposition> dispositionRepo,
            IRepository<Loan> loanRepo,
            IRepository<Transaction> transactionRepo,
            IMapper mapper)
        {
            _accountRepo = accountRepo;
            _TypeRepo = typeRepo;
            _cardRepo = cardRepo;
            _customerRepo = customerRepo;
            _dispositionRepo = dispositionRepo;
            _loanRepo = loanRepo;
            _transactionRepo = transactionRepo;
            _mapper = mapper;
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

        public ApplicationResponce GetAccountInfo(string id)
        {

            var customerID = FindCustomerIdWithUserId(id);
            if (customerID == 0)
            {
                return new()
                {
                    ResponceCode = 404,
                    ResponceText = "Customer not found"
                };
            }

            var customer = _customerRepo.GetById(customerID);

            var CustomerDispositions =
                _dispositionRepo
                .GetAll()
                .Where(x => x.CustomerId == customerID);

            CustomerDetails bankCustomer = new()
            {
                CustomerInfo = CustomMapper.MapDTO<Customer, CustomerDTO>(customer)
            };

            foreach (var item in CustomerDispositions)
            {
                bankCustomer.Accounts
                    .Add(CustomMapper.MapDTO<Account, AccountDTO>(item.Account));
                List<CardDTO> cards = new();
                foreach (var card in item.Cards)
                {
                    cards.Add(CustomMapper.MapDTO<Card, CardDTO>(card));
                };
                bankCustomer.Cards
                    .AddRange(cards);
                List<LoanDTO> loans = new();
                foreach (var loan in item.Account.Loans)
                {
                    loans.Add(CustomMapper.MapDTO<Loan, LoanDTO>(loan));
                }
                bankCustomer.Loans
                    .AddRange(loans);
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

        private int FindCustomerIdWithUserId(string id)
        {
            var result = _customerRepo.GetAll().FirstOrDefault(c => c.ApplicationUserId == id);
            if (result == null) return 0;
            return result.CustomerId;
        }
    }
}