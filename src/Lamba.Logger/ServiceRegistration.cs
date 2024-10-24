using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Polly;
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
                    .Enrich.WithEnvironmentName();

                var elasticUri = context.Configuration["ElasticConfigurations:Uri"]!;
                var username = context.Configuration["ElasticConfigurations:Username"]!;
                var password = context.Configuration["ElasticConfigurations:Password"]!;

                var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetry(
                        retryCount: 5,
                        sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                        onRetry: (exception, timeSpan, retryCount, context) =>
                        {
                            Console.WriteLine($"Error: {exception.Message}");
                            Console.WriteLine($"Retrying Elasticsearch connection: Attempt {retryCount}. Waiting {timeSpan.TotalSeconds} seconds.");
                        });

                policy.Execute(() =>
                {
                    configuration.WriteTo.Elasticsearch(
                        [new Uri(elasticUri)],
                        opt =>
                        {
                            opt.DataStream = new DataStreamName("logs", dataset, context.HostingEnvironment.EnvironmentName);
                            opt.BootstrapMethod = BootstrapMethod.Failure;
                        },
                        transport =>
                        {
                            transport.Authentication(new BasicAuthentication(username, password));
                        }, restrictedToMinimumLevel: LogEventLevel.Error);
                });

                if (context.HostingEnvironment.IsDevelopment() || context.HostingEnvironment.IsEnvironment("Local"))
                {
                    configuration.WriteTo.Console(LogEventLevel.Information);
                }
            });
        }
    }
}
