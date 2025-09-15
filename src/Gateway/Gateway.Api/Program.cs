using Gateway.Api;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.ConfigureOcelot();

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

//// This should be last to catch all other requests.
await app.UseOcelot();

app.Run();