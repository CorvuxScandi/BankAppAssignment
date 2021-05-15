using BankApp.Application.Interfaces;
using BankApp.Application.Services;
using BankApp.Data.Reposetories;
using BankApp.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BankApp.Infrastructure.IoC
{
    public class DependicyContainer
    {
        public static void RegisterServises(IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
