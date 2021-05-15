using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Domain.Interfaces;

namespace BankApp.Application.Services
{
    public class AccountService : IAccountService
    {
        public IAccountRepository _accountRepo;

        public AccountService(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public AccountApiModel GetAccounts()
        {
            return new AccountApiModel()
            {
                Accounts = _accountRepo.GetAccounts()
            };
        }
    }
}
