using Lamba.Cache.Abstract;
using Lamba.Cache.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Lamba.Cache
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddLambaCacheServices(this IServiceCollection services)
        {
            services.AddSingleton<ILambaCacheManager, RedisCacheManager>();
            return services;
        }
    }
}
