using Book2Glow.Infrastructure.Extension;
using Book2Glow.Service.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Book2Glow.Service.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services, string connexionString)
        {
            services.AddInfrastructure(connexionString);
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ICategoryService, CategoryService>();

        }
    }
}
