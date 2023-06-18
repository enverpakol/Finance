using Finance.Application.Abstractions;
using Finance.Infastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Infastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfastructureServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IAppInit, AppInit>();
        }
    }
}
