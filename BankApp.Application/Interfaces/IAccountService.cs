using BankApp.Application.ApiModels;

namespace BankApp.Application.Interfaces
{
    public interface IAccountService
    {
        AccountApiModel GetAccounts();
    }
}
