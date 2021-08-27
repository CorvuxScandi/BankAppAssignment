using BankApp.Application.Interfaces;
using BankApp.Application.Services;
using BankApp.Data.Reposetories;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BankApp.Infrastructure.IoC
{
    public class DependicyContainer
    {
        public static void RegisterServises(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAdminService, AdminServices>();
            services.AddScoped<IRepository<Account>, AccountRepository>();
            services.AddScoped<IRepository<AccountType>, AccountTypeRepository>();
            services.AddScoped<IRepository<Card>, CardRepository>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Disposition>, DispositionRepository>();
            services.AddScoped<IRepository<Loan>, LoanRepository>();
            services.AddScoped<IRepository<Transaction>, TransactionRepository>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}