namespace Setups.Infrastructure;

using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Setups.Infrastructure.Context;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase<SetupsContext>();

        MigrateDatabase(configuration);

        return services;
    }

    private static void MigrateDatabase(IConfiguration configuration)
    {
        var migrationConnectionString = configuration.GetConnectionString("MigratorConnection");

        var optionsBuilder = new DbContextOptionsBuilder<SetupsContext>();
        optionsBuilder.UseSqlServer(migrationConnectionString);
        var dbContext = new SetupsContext(optionsBuilder.Options);

        dbContext.Database.Migrate();
    }
}
