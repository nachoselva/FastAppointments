namespace Common.Application
{
    using Common.Application.CQRS.Implementations;
    using Common.Application.CQRS;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services, Assembly asssembly)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddAllImplementationOf(typeof(ICommandHandler<,>), asssembly);
            services.AddAllImplementationOf(typeof(ICommandValidator<,>), asssembly);
            services.AddAllImplementationOf(typeof(IQueryHandler<,>), asssembly);
            services.AddAllImplementationOf(typeof(IQueryValidator<,>), asssembly);

            return services;
        }

        private static IServiceCollection AddAllImplementationOf(this IServiceCollection services, Type type, Assembly assembly)
        {
            var type1 = typeof(ICommandHandler<,>);   

            var handlerTypes = assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract &&
                                    t.GetInterfaces().Any(i => i.IsGenericType &&
                                                                 i.GetGenericTypeDefinition() == type));

            foreach (var handlerType in handlerTypes)
            {
                var implementedInterfaces = handlerType.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == type);

                foreach (var implementedInterface in implementedInterfaces)
                {
                    services.AddScoped(implementedInterface, handlerType);
                }
            }

            return services;
        }
    }
}
