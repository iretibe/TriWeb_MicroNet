using Consul;
using MicroNet.Branch.Api.Branches.CreateBranch;
using MicroNet.Branch.Api.Branches.DeleteBranch;
using MicroNet.Branch.Api.Branches.GetAllBranches;
using MicroNet.Branch.Api.Branches.GetBranchById;
using MicroNet.Branch.Api.Branches.GetBranchNameById;
using MicroNet.Branch.Api.Branches.UpdateBranch;
using MicroNet.Branch.Api.BranchTerminationRules.CreateBranchTerminationRule;
using MicroNet.Branch.Api.BranchTerminationRules.DeleteBranchTerminationRule;
using MicroNet.Branch.Api.BranchTerminationRules.GetAllBranchTerminationRules;
using MicroNet.Branch.Api.BranchTerminationRules.GetBranchTerminationRuleById;
using MicroNet.Branch.Api.BranchTerminationRules.UpdateBranchTerminationRule;
using MicroNet.Branch.Api.Data;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared;
using MicroNet.Shared.Consul.ServiceDiscovery;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BranchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllBranchesQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetBranchByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBranchCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateBranchCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteBranchCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetBranchNameByIdQuery).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllBranchTerminationRulesQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetBranchTerminationRuleByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBranchTerminationRuleCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateBranchTerminationRuleCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteBranchTerminationRuleCommand).Assembly));

var assembly = typeof(Program).Assembly;

builder.Services
    .AddCommandHandlers(assembly)
    .AddQueryHandlers(assembly)
    .AddInMemoryCommandDispatcher()
    .AddInMemoryQueryDispatcher()
    .AddEventHandlers()
    .AddInMemoryEventDispatcher();

// Consul Implementation
builder.Services.AddConsulServiceDiscovery(builder.Configuration["consul:Address"]!);

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
                    {"micronetbranchapi", "MicroNet Branch Api - full access"}
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
            new[] { "micronetbranchapi" }
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

builder.Services
    .AddScoped<IBranchRepository, BranchRepository>()
    .AddScoped<IBranchTerminationRuleRepository, BranchTerminationRuleRepository>();

//builder.Services.AddScoped<IBranchCacheService, BranchCacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroNet Branch Api");
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
        options.SwaggerEndpoint(builder.Configuration["AppSettings:Folder"] + "/swagger/v1/swagger.json", "MicroNet Branch Api");
        options.OAuthClientId(builder.Configuration["MicroNetIdpSettings:ApiName"]);
        options.OAuthRealm(" ");
        options.OAuthAppName(" ");
        options.OAuthUsePkce();
    });

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

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapControllers();

await app.RunAsync();