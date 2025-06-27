using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

// Load reverse proxy configuration from appsettings.json
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Configure JWT Bearer authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var idpSettings = builder.Configuration.GetSection("MicroNetIdpSettings");

        options.Authority = idpSettings["Authority"];
        options.RequireHttpsMetadata = bool.Parse(idpSettings["RequireHttpsMetadata"] ?? "true");
        options.Audience = idpSettings["ApiName"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = idpSettings["IssuerUri"],
            ValidateAudience = true,
            ValidAudience = idpSettings["ApiName"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();