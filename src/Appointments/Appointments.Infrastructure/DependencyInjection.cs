namespace Appointments.Infrastructure;

using Appointments.Application.Abstractions;
using Appointments.Infrastructure.Context;
using Appointments.Infrastructure.Events;
using Appointments.Infrastructure.Implementations;
using Common.Infrastructure;
using Common.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase<AppointmentsContext>();
        services.AddScoped<IRecurrenceRepository, RecurrenceRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddEventConnection(configuration);
        services.AddEventPublisher<AppointmentsEventPublisher, PendingBillEventBody>();

        MigrateDatabase(configuration);

        return services;
    }

    private static void MigrateDatabase(IConfiguration configuration)
    {
        var migrationConnectionString = configuration.GetConnectionString("MigratorConnection");

        var optionsBuilder = new DbContextOptionsBuilder<AppointmentsContext>();
        optionsBuilder.UseSqlServer(migrationConnectionString);
        var dbContext = new AppointmentsContext(optionsBuilder.Options);

        dbContext.Database.Migrate();
    }
}
