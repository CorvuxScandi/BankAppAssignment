using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using System.Threading.Tasks;

namespace BankApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUser(LoginDTO userLogin);

        Task<string> CreateToken();
    }
}