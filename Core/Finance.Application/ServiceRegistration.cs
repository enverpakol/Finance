using Finance.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapProfile));
        }
    }
}
