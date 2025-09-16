using Gateway.Api.Config;
using Microsoft.Extensions.Configuration;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using static Gateway.Api.Controllers.OpenApiController;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddControllers();

//builder.Configuration.AddOcelot("ocelot.json"); // step 2
//builder.Services.AddOcelot(builder.Configuration); // step 3
//// Add services to the container.

ConfigureOcelot(builder);

var app = builder.Build();

app.UseRouting();

app.UseAuthorization();

// It is required to use UseEndpoints to avoid a pipeline conflict with ocelot 

#pragma warning disable ASP0014 // Suggest using top level route registrations

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

#pragma warning restore ASP0014 // Suggest using top level route registrations

await app.UseOcelot();

app.Run();


static void ConfigureOcelot(WebApplicationBuilder builder)
{
    var fileConfig = builder.Configuration.Get<FileConfiguration>() ?? new FileConfiguration();

    fileConfig.GlobalConfiguration ??= new FileGlobalConfiguration();
    fileConfig.GlobalConfiguration.BaseUrl = "http://localhost:5009";

    var apiConfigs = builder.Configuration.GetRequiredSection("Apis").Get<ApiConfig[]>() ?? [];

    //foreach (var item in apiConfigs)
    //{
    //    // Add a specific route for the root path (e.g., /appointments or /payments)
    //    fileConfig.Routes.Add(new FileRoute
    //    {
    //        RouteIsCaseSensitive = false,
    //        UpstreamPathTemplate = $"/{item.Path}",
    //        UpstreamHttpMethod = ["GET", "POST", "PUT", "DELETE"],
    //        DownstreamPathTemplate = "/",
    //        DownstreamScheme = "http",
    //        DownstreamHostAndPorts = new List<FileHostAndPort>
    //        {
    //            new() { Host = item.Url, Port = item.Port }
    //        }
    //    });

    //    // Add a catch-all route for all other paths
    //    fileConfig.Routes.Add(new FileRoute
    //    {
    //        RouteIsCaseSensitive = false,
    //        UpstreamPathTemplate = $"/{item.Path}/{{*catchAll}}",
    //        UpstreamHttpMethod = ["GET", "POST", "PUT", "DELETE"],
    //        DownstreamPathTemplate = "/{*catchAll}",
    //        DownstreamScheme = "http",
    //        DownstreamHostAndPorts = new List<FileHostAndPort>
    //        {
    //            new() { Host = item.Url, Port = item.Port }
    //        }
    //    });
    //}

    fileConfig.Routes.Add(new FileRoute
    {
        RouteIsCaseSensitive = false,
        UpstreamPathTemplate = $"/payments/Bill",
        UpstreamHttpMethod = ["GET", "POST", "PUT", "DELETE"],
        DownstreamPathTemplate = "/Bill",
        DownstreamScheme = "http",
        DownstreamHostAndPorts = new List<FileHostAndPort>
            {
                new() { Host = "payments.api", Port = 8080 }
            }
    });

    builder.Configuration.AddOcelot(fileConfig);
    builder.Services.AddOcelot();
}

