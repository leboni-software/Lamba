using Lamba.Cache.Abstract;
using Lamba.Cache.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
