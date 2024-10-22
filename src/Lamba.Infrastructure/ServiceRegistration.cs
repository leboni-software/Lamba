using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lamba.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddLambaInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentEnvironment)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
