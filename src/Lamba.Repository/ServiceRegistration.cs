using Lamba.Infrastructure;
using Lamba.Repository.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lamba.Repository
{
    public static class ServiceRegistration
    {
        public static void AddLambaRepositoryServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentEnvironment)
        {
            services.AddLambaInfrastructureServices(configuration, isDevelopmentEnvironment);
            Assembly.GetCallingAssembly().GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,,>)))
            .ToList()
            .ForEach(repository =>
            {
                services.AddScoped(repository.GetInterfaces().Last(), repository);
            });
            Assembly.GetCallingAssembly().GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IUnitOfWork)))
            .ToList()
            .ForEach(uow =>
            {
                services.AddScoped(uow.GetInterfaces().Last(), uow);
            });
        }
    }
}
