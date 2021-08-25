using BankApp.Data;
using BankApp.Data.Contexts;
using BankApp.Domain.IdentityModels;
using BankApp.Infrastructure.IoC;
using BankApp.Web.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankApp.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.ConfigureCors();
            services.ConfigureIISIntegration();

            services.ConfigureDataContext(Configuration);
            services.ConfigureIdentityContext(Configuration);
            services.ConfigureIdentity();

            RegisterServices(services);
            services.AddAutoMapper(typeof(Startup));


            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme =
            //      JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme =
            //    JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters =
            //    new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,

            //        ValidAudience = Configuration["JWT:ValidAudience"],
            //        ValidIssuer = Configuration["JWT:ValidIssuer"],
            //        IssuerSigningKey =
            //        new SymmetricSecurityKey
            //    (Encoding.UTF8.GetBytes(Configuration["JWT:securityKey"]))
            //    };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<ApplicationUser> user, BankAppDataContext context, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //InitiateAccounts.SeedData(user, roleManager, context);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            DependicyContainer.RegisterServises(services);
        }
    }
}