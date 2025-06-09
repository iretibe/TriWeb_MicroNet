using MicroNet.Pos.Api.Jobs;
using MicroNet.Pos.Application;
using MicroNet.Pos.Infrastructure;
using MicroNet.Pos.Infrastructure.Data;
using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.Shared.CQRS.Events;
using MicroNet.Shared.Messaging.RabbitMq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddDbContext<PosContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:PosConnection"]));

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddScoped<IDispatcher, InMemoryDispatcher>();
builder.Services.AddSingleton<IMessageBroker, RabbitMQMessageBroker>();

builder.Services.AddHostedService<OutboxProcessor>();

builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["OtherServiceUrls:UserServiceUrl"]!);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddDistributedMemoryCache();

//builder.Services
//    .AddScoped<AuditLogServiceClient>();

//builder.Services.AddScoped<IAuditLogServiceClient, AuditLogServiceClient>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

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
                    {"micronetposapi", "MicroNet Pos Api - full access"}
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
            new[] { "micronetposapi" }
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
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroNet Pos Api");
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
        options.SwaggerEndpoint(builder.Configuration["AppSettings:Folder"] + "/swagger/v1/swagger.json", "MicroNet Pos Api");
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
