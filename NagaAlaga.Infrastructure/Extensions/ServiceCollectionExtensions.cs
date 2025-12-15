using Microsoft.Extensions.DependencyInjection;
using NagaAlaga.Infrastructure.Services;

namespace NagaAlaga.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ISupabaseMedicationService, SupabaseMedicationService>();
            return services;
        }
    }
}
