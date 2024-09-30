using Lamba.Infrastructure;
using Lamba.Repository.Abstract;
using Lamba.Repository.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lamba.Repository
{
    public static class ServiceRegistration
    {
        public static void AddLambaRepositoryServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentEnvironment)
        {
            services.AddLambaInfrastructureServices(configuration, isDevelopmentEnvironment);
            services.AddScoped(typeof(IRepository<,,>), typeof(BaseRepository<,,>));
            services.AddScoped(typeof(IReaderRepository<,>), typeof(BaseReaderRepository<,>));
            services.AddScoped(typeof(IWriterRepository<,>), typeof(BaseWriterRepository<,>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<>));
        }
    }
}
