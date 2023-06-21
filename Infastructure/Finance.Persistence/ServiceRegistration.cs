using Finance.Application.Repositories;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Finance.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<AppData>(options => options.UseMySql(
                Configurations.ConnectionString,
                ServerVersion.AutoDetect(Configurations.ConnectionString)
                ));

            services.AddIdentity<AppUser, AppRole>(config =>
            {
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                config.Password.RequiredLength = 6;
                config.Password.RequireNonAlphanumeric = false;
                config.SignIn.RequireConfirmedEmail = false;
                config.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppData>();


            services.AddScoped<IAppUserRepository, AppUserRepository>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
            services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
        }
    }
}
