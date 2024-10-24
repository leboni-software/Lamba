using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Lamba.Logger
{
    public static class ServiceRegistration
    {
        public static void UseLambaLogger(this ConfigureHostBuilder host, string dataset)
        {
            host.UseSerilog((context, configuration) =>
            {
                configuration
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .WriteTo.Elasticsearch([new Uri(context.Configuration["ElasticConfigurations:Uri"]!)],
                opt =>
                {
                    opt.DataStream = new DataStreamName("logs", dataset, context.HostingEnvironment.EnvironmentName);
                    opt.BootstrapMethod = BootstrapMethod.Failure;
                },
                transport =>
                {
                    transport.Authentication(
                        new BasicAuthentication(
                            context.Configuration["ElasticConfigurations:Username"]!,
                            context.Configuration["ElasticConfigurations:Password"]!)
                        );
                }, restrictedToMinimumLevel: LogEventLevel.Error);
                if (context.HostingEnvironment.IsDevelopment() || context.HostingEnvironment.IsEnvironment("Local"))
                    configuration.WriteTo.Console(LogEventLevel.Information);
            });
        }
    }
}
