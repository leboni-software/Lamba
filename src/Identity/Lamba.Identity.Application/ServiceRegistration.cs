using Lamba.Identity.Application.Common.Behaviors;
using Lamba.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizePipelineBehavior<,>));
        }
    }
}
