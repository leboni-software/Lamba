using Microsoft.Extensions.DependencyInjection;
using Lamba.Security;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Lamba.Identity.Application
{
    public static class ServiceRegistration
    {
        public static void AddLambaIdentityApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLambaSecurityServices(configuration);
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
        }
    }
}
