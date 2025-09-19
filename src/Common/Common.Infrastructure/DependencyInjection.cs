namespace Common.Infrastructure
{
    using Common.Application.Repositories;
    using Common.Infrastructure.Clients;
    using Common.Infrastructure.Configuration;
    using Common.Infrastructure.Context;
    using Common.Infrastructure.Events;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Payments.Application.Abstractions;
    using RabbitMQ.Client;
    using System.Net.NetworkInformation;

    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {
            services.AddDbContext<T>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork<T>>();
            return services;
        }

        public static IServiceCollection AddEventConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQSettings = configuration.GetSection("RabbitMQSettings")!;

            services.AddSingleton(sp => new ConnectionFactory()
            {
                HostName = rabbitMQSettings["Host"]!,
                Port = int.Parse(rabbitMQSettings["Port"]!),
                UserName = rabbitMQSettings["UserName"]!,
                Password = rabbitMQSettings["Password"]!
            });

            return services;
        }

        public static IServiceCollection AddEventReceiver<TReceiver, TEvent>(this IServiceCollection services)
            where TReceiver : EventReceiver<TEvent>
        {
            services.AddSingleton<TReceiver>();

            services.AddHostedService(sp => sp.GetRequiredService<TReceiver>());

            return services;
        }

        public static IServiceCollection AddEventPublisher<TImplementation, TEvent>(this IServiceCollection services)
            where TImplementation : EventPublisher<TEvent>
        {
            services.AddSingleton<IEventPublisher<TEvent>, TImplementation>();

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var apis = configuration.GetApisConfigByName();
            services.AddClient<AppointmentsClient>(apis, "appointments");
            services.AddClient<PaymentsClient>(apis, "payments");
            return services;
        }

        private static void AddClient<T>(this IServiceCollection services, IDictionary<string, ApiConfig> configs, string apiName)
            where T : class
        {
            services.AddHttpClient<T>(client =>
            {
                var config = configs[apiName];
                client.BaseAddress = new Uri($"http://{config.Host}:{config.HttpPort}");
            });
        }
    }
}
