namespace Common.Infrastructure
{
    using Common.Application;
    using Common.Application.Repositories;
    using Common.Infrastructure.Configuration;
    using Common.Infrastructure.Context;
    using Common.Infrastructure.Events;
    using Common.Infrastructure.HttpClients;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RabbitMQ.Client;

    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase<T>(this IServiceCollection services, Action<DbContextOptionsBuilder>? dbOptions = null)
            where T : DbContext
        {
            services.AddDbContext<T>(dbOptions);
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
            services.AddClient<SetupsClient>(apis, "setups");
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
