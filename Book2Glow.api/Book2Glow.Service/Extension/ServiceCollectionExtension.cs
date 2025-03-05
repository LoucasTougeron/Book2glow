using Book2Glow.Infrastructure.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace Book2Glow.Service.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services, string connexionString)
        {
            services.AddInfrastructure(connexionString);
        }
    }
}
