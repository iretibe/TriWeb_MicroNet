using Consul;
using MicroNet.Shared;
using MicroNet.Shared.Consul.ServiceDiscovery;
using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.Shared.CQRS.Events;
using MicroNet.Shared.Messaging.RabbitMq;
using MicroNet.User.Application;
using MicroNet.User.Core.Helper;
using MicroNet.User.Core.Logging;
using MicroNet.User.Core.Models;
using MicroNet.User.Infrastructure;
using MicroNet.User.Infrastructure.Clients.Branch;
using MicroNet.User.Infrastructure.Clients.Menu;
using MicroNet.User.Infrastructure.Data;
using MicroNet.User.Infrastructure.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Polly;
using Prometheus;
using Serilog;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Loggin with Serilog (Seq)
Log.Logger = new LoggerConfiguration()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .Enrich.FromLogContext()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration["Observability:Seq"]!) // Seq default URL
    .ReadFrom.Configuration(builder.Configuration) // Support configuration from appsettings.json
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
    {
        resource.AddService("MicroNet.User.Api", serviceVersion: "1.0.0");
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddPrometheusExporter()
            .AddOtlpExporter(otlp =>
            {
                otlp.Endpoint = new Uri(builder.Configuration["Observability:Jaeger"]!); // OTLP over gRPC
                otlp.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
            });
    });

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:UserConnection"]));

builder.Services.AddDbContext<IdentityUserContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:UserConnection"]));

builder.Services.AddDbContext<ApplicationDbContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
        builder.Configuration["ConnectionStrings:UserConnection"]));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:UserConnection"]));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    //options.Password.RequiredLength = 12;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddScoped<IDispatcher, InMemoryDispatcher>();
builder.Services.AddSingleton<IMessageBroker, RabbitMQMessageBroker>();

builder.Services.AddScoped<IDomainEventLogger, DomainEventLogger>();

builder.Services.AddHealthChecks();

builder.Services.AddControllers();

List<string> urlList = builder.Configuration.GetSection("WebClients:Links").Get<List<string>>()!;
string[] clientUrls = urlList!.Select(i => i.ToString()).ToArray();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(clientUrls)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["MicroNetIdpSettings:AuthorityUrl"]!),
                TokenUrl = new Uri(builder.Configuration["MicroNetIdpSettings:TokenUrl"]!),
                RefreshUrl = new Uri(builder.Configuration["MicroNetIdpSettings:TokenUrl"]!),
                Scopes = new Dictionary<string, string>
                {
                    {"micronetuserapi", "MicroNet User Api - full access"}
                }
            },
        }
    });

    // Apply Scheme globally
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new[] { "micronetuserapi" }
        }
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["MicroNetIdpSettings:Authority"];
        options.RequireHttpsMetadata = bool.Parse(builder.Configuration["MicroNetIdpSettings:RequireHttpsMetadata"]!);
        options.SaveToken = bool.Parse(builder.Configuration["MicroNetIdpSettings:SaveToken"]!);
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.FromSeconds(0)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "WebAPI");
    });
});

builder.Services.AddOpenApi();

builder.Services.AddHttpClient("MenuService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["OtherServiceUrls:MenuServiceUrl"]!);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
})
    .AddTransientHttpErrorPolicy(policy => policy
    .WaitAndRetryAsync(3, retryAttempt =>
        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), onRetry: (outcome, timespan, retryCount, context) =>
        {
            Console.WriteLine($"Retry {retryCount} after {timespan.TotalSeconds}s due to {outcome.Exception?.Message}");
        }));

builder.Services.AddHttpClient("BranchService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["OtherServiceUrls:BranchServiceUrl"]!);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services
    .AddScoped<MenuServiceClient>()
    .AddScoped<BranchServiceClient>();

//// Consul Implementation
//builder.Services.AddConsulServiceDiscovery(builder.Configuration["consul:Address"]!);

// Consul & Fabio Registration
builder.Services.AddConsulFabio(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroNet User Api");
        options.OAuthClientId(builder.Configuration["MicroNetIdpSettings:ApiName"]);
        options.OAuthRealm(" ");
        options.OAuthAppName(" ");
        options.OAuthUsePkce();
    });

    app.MapOpenApi();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(builder.Configuration["AppSettings:Folder"] + "/swagger/v1/swagger.json", "MicroNet User Api");
        options.OAuthClientId(builder.Configuration["MicroNetIdpSettings:ApiName"]);
        options.OAuthRealm(" ");
        options.OAuthAppName(" ");
        options.OAuthUsePkce();
    });

    app.MapOpenApi();
}

//var consultClient = app.Services.GetRequiredService<IConsulClient>();

//var registry = new ConsulServiceRegistry(
//    consultClient, 
//    builder.Configuration["consul:ServiceName"]!,
//    builder.Configuration["consul:Host"]!, 
//    Convert.ToInt32(builder.Configuration["consul:Port"]));

// Register service with Consul + Fabio tag
var registry = new ConsulServiceRegistry(builder.Configuration);

// Register the service on startup
await registry.RegisterAsync();

app.Lifetime.ApplicationStopping.Register((() =>
{
    registry.DeregisterAsync().GetAwaiter().GetResult();
}));

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ImgFolder")),
    RequestPath = "/ImgFolder"
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ImgFolder")),
    RequestPath = "/ImgFolder"
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapControllers();

app.UseSerilogRequestLogging(); // Optional but useful for HTTP logs

// Register service in Consul/Fabio
await app.UseConsulFabio(builder.Configuration, app.Lifetime);

//app.UseOpenTelemetryPrometheusScrapingEndpoint();

await app.RunAsync();
