using Lamba.Infrastructure;
using Lamba.Repository.Abstract;
using Lamba.Repository.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lamba.Repository
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddLambaRepositoryServices(this IServiceCollection services, IConfiguration configuration, bool isDebug)
        {
            services.AddLambaInfrastructureServices(configuration, isDebug);
            services.AddScoped(typeof(IRepository<,,>), typeof(BaseRepository<,,>));
            services.AddScoped(typeof(IReaderRepository<,>), typeof(BaseReaderRepository<,>));
            services.AddScoped(typeof(IWriterRepository<,>), typeof(BaseWriterRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
