using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankApp.Frontend.Services
{
    public interface IClientService
    {
        IEnumerable<Claim> GetTokenClaims(string token);

        Task<HttpResponseMessage> CallAPI(string endURI, object value);

        Task<HttpResponseMessage> CallAPI(string endURI);
    }
}