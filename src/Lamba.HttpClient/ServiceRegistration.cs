using Lamba.HttpClient.Abstract;
using Lamba.HttpClient.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Lamba.HttpClient
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddLambaHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<ILambaHttpClient, LambaHttpClient>();
            return services;
        }
    }
}
