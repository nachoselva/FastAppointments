using Common.Api;
using Payments.Application;
using Payments.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => options.AddFilters());

builder.Services.AddOpenApi();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
