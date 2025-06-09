using MicroNet.Device.Application;
using MicroNet.Device.Core.Logging;
using MicroNet.Device.Infrastructure;
using MicroNet.Device.Infrastructure.Data;
using MicroNet.Device.Infrastructure.Logging;
using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.Shared.CQRS.Events;
using MicroNet.Shared.Messaging.RabbitMq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddDbContext<DeviceContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DeviceConnection"]));

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddScoped<IDispatcher, InMemoryDispatcher>();
builder.Services.AddSingleton<IMessageBroker, RabbitMQMessageBroker>();

builder.Services.AddScoped<IDomainEventLogger, DomainEventLogger>();

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
                    {"micronetdeviceapi", "MicroNet Device Api - full access"}
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
            new[] { "micronetdeviceapi" }
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroNet Device Api");
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
        options.SwaggerEndpoint(builder.Configuration["AppSettings:Folder"] + "/swagger/v1/swagger.json", "MicroNet Device Api");
        options.OAuthClientId(builder.Configuration["MicroNetIdpSettings:ApiName"]);
        options.OAuthRealm(" ");
        options.OAuthAppName(" ");
        options.OAuthUsePkce();
    });

    app.MapOpenApi();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
