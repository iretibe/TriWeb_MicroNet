using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using MicroNet.Identity.Data;
using MicroNet.Identity.Extensions;
using MicroNet.Identity.Models;
using MicroNet.Identity.Options;
using MicroNet.Identity.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.Security.Cryptography.X509Certificates;

namespace MicroNet.Identity
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSameSiteCookiePolicy();

            var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging() // Enable detailed logging
                .LogTo(Console.WriteLine, LogLevel.Information));

            //builder.Services.AddDbContext<MicroNetContext>(options =>
            //    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            var isBuilder = builder.Services
                .AddIdentityServer(options =>
                {
                    // IdentityServer configuration
                    options.UserInteraction.LoginUrl = "/Account/Login";
                    options.UserInteraction.LogoutUrl = "/Account/Logout";

                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                    options.EmitStaticAudienceClaim = true;

                    //Token Lifetimes
                    options.Authentication.CookieLifetime = TimeSpan.FromMinutes(60); //Set cookie lifetime
                    options.Authentication.CookieSlidingExpiration = true; //Enable sliding expiration
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddConfigurationStoreCache()
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddAspNetIdentity<ApplicationUser>();

            //Session Management:
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "IdentityServerCookie";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddSession();
            builder.Services.AddScoped<SessionManagementService>();

            isBuilder.AddServerSideSessions();

            var cert = new X509Certificate2(Path.Combine(builder.Environment.ContentRootPath, "psl-webapps.pfx"), "Persol@123");

            builder.Services.AddDataProtection()
                .PersistKeysToDbContext<ApplicationDbContext>()
                .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            //This is the code that adds migration of sessions.Enabling server side sessions through the
            //block above without enabling this will invalidate all existing sessions.
            builder.Services.AddTransient<IPostConfigureOptions<CookieAuthenticationOptions>, SessionMigrationPostConfigureOptions>();

            // Add MFA and SSO services as needed
            // For example, to add Google authentication:
            builder.Services.AddAuthentication()
                .AddOpenIdConnect("Google", "Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = "https://accounts.google.com/";
                    options.ClientId = "235642643039-0ufg9qeidobgin64cdikuk737c30mqb9.apps.googleusercontent.com";

                    options.CallbackPath = "/signin-google";
                    options.Scope.Add("email");
                    options.MapInboundClaims = false;
                })
                .AddOpenIdConnect("Facebook", "Facebook", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = "https://web.facebook.com/";
                    options.ClientId = "1528646091338813";

                    options.CallbackPath = "/signin-facebook";
                    options.Scope.Add("email");
                    options.MapInboundClaims = false;
                });

            //builder.Services.AddSingleton<DapperContext>();

            //builder.Services
            //    .AddScoped<IEmailRepository, EmailRepository>()
            //    .AddScoped<IUserRepository, UserRepository>();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //InitializeDatabase(app);

            app.UseSession();
            app.UseMiddleware<SessionValidationMiddleware>();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();


            app.MapRazorPages()
                .RequireAuthorization();

            return app;
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.ApiResources)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}