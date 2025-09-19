namespace Setups.Application;

using Common.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCQRS(Assembly.GetExecutingAssembly());
        return services;
    }
}
