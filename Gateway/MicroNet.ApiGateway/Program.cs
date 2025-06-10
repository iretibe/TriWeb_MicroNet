using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using Yarp.ReverseProxy.Transforms;

namespace MicroNet.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = builder.Configuration["MicroNetIdpSettings:Authority"];
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
                .AddTransforms(builderContext =>
                {
                    builderContext.AddRequestTransform(async transformContext =>
                    {
                        var accessToken = await transformContext.HttpContext.GetTokenAsync("access_token");
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            transformContext.ProxyRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                        }
                    });
                });

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapReverseProxy();

            app.MapGet("/", () => "Hello World! Welcome to MicroNet API Gateway service").RequireAuthorization();

            app.Run();
        }
    }
}
