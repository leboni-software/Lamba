using Lamba.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddLambaInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool isDebug)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);            
            services.AddDbContext<BaseWriterDbContext>(opt =>
            {
                opt.EnableDetailedErrors(isDebug);
                opt.EnableSensitiveDataLogging(isDebug);
                opt.UseNpgsql(configuration.GetConnectionString("WriterConnectionString"), sql => sql.EnableRetryOnFailure(3));
            });
            services.AddDbContext<BaseReaderDbContext>(opt =>
            {
                opt.EnableDetailedErrors(isDebug);
                opt.EnableSensitiveDataLogging(isDebug);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opt.UseNpgsql(configuration.GetConnectionString("WriterConnectionString"), sql => sql.EnableRetryOnFailure(3));
            });
            return services;
        }
    }
}
