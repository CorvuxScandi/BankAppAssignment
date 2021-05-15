using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Domain.Interfaces;

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
    }
}
