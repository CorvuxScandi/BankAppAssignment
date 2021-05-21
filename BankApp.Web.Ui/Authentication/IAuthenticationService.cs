using BankApp.Web.Ui.Models;
using System.Threading.Tasks;

namespace BankApp.Web.Ui.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel authenticationModel);
        Task Logout();
    }
}