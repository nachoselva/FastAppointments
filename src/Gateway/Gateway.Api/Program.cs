using Gateway.Api;
using Ocelot.Configuration.Repository;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


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

await app.UseOcelot();

app.Run();