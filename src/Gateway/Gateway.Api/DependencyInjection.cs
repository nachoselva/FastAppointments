namespace Gateway.Api
{
    using Gateway.Api.Config;
    using Ocelot.Configuration.File;
    using Ocelot.Configuration.Repository;
    using static Gateway.Api.Controllers.OpenApiController;

    internal static class DependencyInjection
    {
        public static void ConfigureOcelot(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            var fileConfig = builder.Configuration.Get<FileConfiguration>() ?? new FileConfiguration();
            fileConfig.Routes ??= [];

            var apiConfigs = builder.Configuration.GetRequiredSection("Apis").Get<ApiConfig[]>() ?? [];

            foreach (var item in apiConfigs)
            {
                fileConfig.Routes.Add(new FileRoute
                {
                    UpstreamPathTemplate = $"/{item.Path}/{{everything}}",
                    UpstreamHttpMethod = ["Get", "Post", "Put", "Delete"],
                    DownstreamPathTemplate = "/{everything}",
                    DownstreamScheme = "http",
                    DownstreamHostAndPorts =
                    [
                        new() {
                            Host = item.Url,
                            Port = item.Port
                        }
                    ]
                });
            }

            builder.Services.AddSingleton<IFileConfigurationRepository>(
                new InMemoryFileConfigurationRepository(fileConfig));
        }
    }
}
