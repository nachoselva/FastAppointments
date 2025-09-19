namespace Gateway.Api
{
    using Common.Infrastructure.Configuration;
    using Ocelot.Configuration.File;
    using Ocelot.DependencyInjection;

    internal static class DependencyInjection
    {
        public static void ConfigureOcelot(this WebApplicationBuilder builder)
        {
            var fileConfig = builder.Configuration.Get<FileConfiguration>() ?? new FileConfiguration();

            fileConfig.GlobalConfiguration ??= new FileGlobalConfiguration();
            fileConfig.GlobalConfiguration.BaseUrl = "http://localhost:5000";

            var apiConfigs = builder.Configuration.GetApisConfig();

            foreach (var item in apiConfigs)
            {
                fileConfig.Routes.Add(new FileRoute
                {
                    RouteIsCaseSensitive = false,
                    UpstreamPathTemplate = $"/{item.Path}/{{catchAll}}",
                    UpstreamHttpMethod = ["GET", "POST", "PUT", "DELETE"],
                    DownstreamPathTemplate = "/{catchAll}",
                    DownstreamScheme = "http",
                    DownstreamHostAndPorts =
                    [
                        new() { Host = item.Host, Port = item.HttpPort }
                    ]
                });
            }

            builder.Configuration.AddOcelot(fileConfig);

            builder.Services.AddOcelot();
        }
    }
}
