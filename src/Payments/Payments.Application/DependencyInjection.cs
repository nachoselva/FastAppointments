namespace Payments.Application;

using Common.Application;
using Microsoft.Extensions.DependencyInjection;
using Payments.Application.Implementations.PaymentEntity.Common;
using Payments.Domain.Abstractions;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCQRS(Assembly.GetExecutingAssembly());
        services.AddScoped<IPaymentEntityService, PaymentEntityService>();
        return services;
    }
}
