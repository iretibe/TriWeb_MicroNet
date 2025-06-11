using Consul;
using MicroNet.Shared;
using MicroNet.Shared.Consul.ServiceDiscovery;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Consul Implementation
builder.Services.AddConsulServiceDiscovery(builder.Configuration["consul:Address"]!);

builder.Services.AddHealthChecks();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var consultClient = app.Services.GetRequiredService<IConsulClient>();

var registry = new ConsulServiceRegistry(
    consultClient,
    builder.Configuration["consul:ServiceName"]!,
    builder.Configuration["consul:Host"]!,
    Convert.ToInt32(builder.Configuration["consul:Port"]));

// Register the service on startup
await registry.RegisterAsync();

app.Lifetime.ApplicationStopping.Register((() =>
{
    registry.DeregisterAsync().GetAwaiter().GetResult();
}));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapControllers();

await app.RunAsync();