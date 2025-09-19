namespace Payments.Infrastructure;

using Common.Infrastructure;
using Common.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payments.Application.Abstractions;
using Payments.Infrastructure.Context;
using Payments.Infrastructure.Events;
using Payments.Infrastructure.Implementations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClients(configuration);
        services.AddDatabase<PaymentsContext>();
        services.AddScoped<IBillRepository, BillRepository>();
        services.AddScoped<IPaymentEntityRepository, PaymentEntityRepository>();
        services.AddEventConnection(configuration);
        services.AddEventReceiver<BillEventReceiver, CreateBillEventBody>();
        services.AddEventPublisher<BillEventPublisher, CreateBillEventBody>();
        services.AddEventReceiver<BillPendingReceiver, PendingBillEventBody>();

        MigrateDatabase(configuration);

        return services;
    }

    private static void MigrateDatabase(IConfiguration configuration)
    {
        var migrationConnectionString = configuration.GetConnectionString("MigratorConnection");

        var optionsBuilder = new DbContextOptionsBuilder<PaymentsContext>();
        optionsBuilder.UseSqlServer(migrationConnectionString);
        var dbContext = new PaymentsContext(optionsBuilder.Options);

        dbContext.Database.Migrate();
    }
}
