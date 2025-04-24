using Book2Glow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Book2Glow.Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, string connexionString)
        {
            services.AddDbContext<DataModelContext>(option => option.UseNpgsql(connexionString));



        }
    }
}
