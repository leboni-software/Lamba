using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using Lamba.Identity.Infrastructure.Data.Contexts;
using Lamba.Identity.Infrastructure.Data.Repositories.Readers;
using Lamba.Identity.Infrastructure.Data.Repositories.UoW;
using Lamba.Identity.Infrastructure.Data.Repositories.Writers;
using Lamba.Repository;
using Lamba.Repository.Abstract;
using Lamba.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lamba.Identity.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddLambaIdentityInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentEnvironment)
        {
            services.AddLambaRepositoryServices(configuration, isDevelopmentEnvironment);
            services.AddDbContext<IdentityWriterDbContext>(opt =>
            {
                opt.EnableDetailedErrors(isDevelopmentEnvironment);
                opt.EnableSensitiveDataLogging(isDevelopmentEnvironment);
                opt.UseNpgsql(configuration.GetConnectionString("WriterConnectionString"), sql => sql.EnableRetryOnFailure(3));
            });
            services.AddDbContext<IdentityReaderDbContext>(opt =>
            {
                opt.EnableDetailedErrors(isDevelopmentEnvironment);
                opt.EnableSensitiveDataLogging(isDevelopmentEnvironment);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opt.UseNpgsql(configuration.GetConnectionString("ReaderConnectionString"), sql => sql.EnableRetryOnFailure(3));
            });
            //services.AddScoped<IUserReaderRepository, UserReaderRepository>();
            //services.AddScoped<IUserWriterRepository, UserWriterRepository>();
            //services.AddScoped<IRoleReaderRepository, RoleReaderRepository>();
            //services.AddScoped<IRoleWriterRepository, RoleWriterRepository>();
            //services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
        }
    }
}
