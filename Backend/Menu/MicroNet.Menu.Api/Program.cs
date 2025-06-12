using Consul;
using MicroNet.Menu.Api.Data;
using MicroNet.Menu.Api.Menus.CreateMenu;
using MicroNet.Menu.Api.Menus.DeleteMenu;
using MicroNet.Menu.Api.Menus.GetAllMenus;
using MicroNet.Menu.Api.Menus.GetAllSystemMenus;
using MicroNet.Menu.Api.Menus.GetAllSystemSubMenusById;
using MicroNet.Menu.Api.Menus.GetMenusById;
using MicroNet.Menu.Api.Menus.UpdateMenu;
using MicroNet.Menu.Api.Repositories;
using MicroNet.Menu.Api.Services;
using MicroNet.Shared;
using MicroNet.Shared.Consul.ServiceDiscovery;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MenuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllMenusQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllSystemMenusQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllSystemSubMenusByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMenuByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateMenuCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteMenuCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateMenuCommand).Assembly));

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
                    {"micronetmenuapi", "MicroNet Menu Api - full access"}
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
            new[] { "micronetmenuapi" }
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

builder.Services.AddScoped<IMenuRepository, MenuRepository>();

builder.Services.AddScoped<IMenuCacheService, MenuCacheService>();

builder.Services.AddDistributedMemoryCache();

var redisEndpoints = new List<RedLockEndPoint>
{
    new DnsEndPoint("localhost", 6379) // Use your actual Redis config
};

var redlockFactory = RedLockFactory.Create(redisEndpoints);

builder.Services.AddSingleton<RedLockFactory>(_ => redlockFactory);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroNet Menu Api");
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
        options.SwaggerEndpoint(builder.Configuration["AppSettings:Folder"] + "/swagger/v1/swagger.json", "MicroNet Menu Api");
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

app.UseHttpsRedirection();

app.MapGetAllMenusEndpoint();
app.MapGetMenuByIdEndpoint();
app.MapGetAllSystemMenus();
app.MapGetAllSystemSubMenusById();
app.MapCreateMenuEndpoint();
app.MapUpdateMenuEndpoint();
app.MapDeleteMenuEndpoint();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapControllers();

//InitializeDatabase(app);

//void InitializeDatabase(IApplicationBuilder app)
//{
//    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
//    {
//        var context = serviceScope.ServiceProvider.GetService<MenuContext>();
//        DbInitializer.Initialize(context!);
//    }
//}

await app.RunAsync();
