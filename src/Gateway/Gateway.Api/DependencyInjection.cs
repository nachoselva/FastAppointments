namespace Gateway.Api
{
    using Gateway.Config;
    using Ocelot.Configuration.File;
    using Ocelot.DependencyInjection;

    internal static class DependencyInjection
    {
        public static void ConfigureOcelot(this WebApplicationBuilder builder)
        {
            var fileConfig = builder.Configuration.Get<FileConfiguration>() ?? new FileConfiguration();

            fileConfig.GlobalConfiguration ??= new FileGlobalConfiguration();
            fileConfig.GlobalConfiguration.BaseUrl = "http://localhost:5000";

            var apiConfigs = builder.Configuration.GetRequiredSection("Apis").Get<ApiConfig[]>() ?? [];

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
                        new() { Host = item.Url, Port = item.Port }
                    ]
                });
            }

            builder.Configuration.AddOcelot(fileConfig);

            builder.Services.AddOcelot();
        }
    }
}
