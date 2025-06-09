using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MicroNet.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("micronetapi", "MicroNet System API")
                {
                    ApiSecrets =
                    {
                        new Secret("M!cR0N$#Tufs1jk6k10$".Sha256())
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("micronetaccountapi", "MicroNet System API"),
                new ApiScope("micronetbranchapi", "MicroNet System API"),
                new ApiScope("micronetclientapi", "MicroNet System API"),
                new ApiScope("micronetdeviceapi", "MicroNet System API"),
                new ApiScope("micronetemployeeapi", "MicroNet System API"),
                new ApiScope("micronetloanapi", "MicroNet System API"),
                new ApiScope("micronetposapi", "MicroNet System API"),
                new ApiScope("micronetproductapi", "MicroNet System API"),
                new ApiScope("micronetreportingapi", "MicroNet System API"),
                new ApiScope("micronetrevenueapi", "MicroNet System API"),
                new ApiScope("micronetsessionapi", "MicroNet System API"),
                new ApiScope("micronetsundryapi", "MicroNet System API"),
                new ApiScope("micronetsystemapi", "MicroNet System API"),
                new ApiScope("micronetuserapi", "MicroNet System API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //Account Management Configuration
                new Client
                {
                    ClientId = "micronet.account.api.code",
                    ClientName = "micronet_account_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetaccountapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7238",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7238/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.account.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7238/signout-callback-oidc",
                        "http://localhost/s2.micronet.account.api/signout-callback-oidc",
                        "https://localhost:7238/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.account.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

				//Branch Configuration
                new Client
                {
                    ClientId = "micronet.branch.api.code",
                    ClientName = "micronet_branch_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetbranchapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7221",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7221/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.branch.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7221/signout-callback-oidc",
                        "http://localhost/s2.micronet.branch.api/signout-callback-oidc",
                        "https://localhost:7221/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.branch.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

				//Client Configuration
                new Client
                {
                    ClientId = "micronet.client.api.code",
                    ClientName = "micronet_client_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetclientapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7243",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7243/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.client.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7243/signout-callback-oidc",
                        "http://localhost/s2.micronet.client.api/signout-callback-oidc",
                        "https://localhost:7243/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.client.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

				//Device Configuration
                new Client
                {
                    ClientId = "micronet.device.api.code",
                    ClientName = "micronet_device_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetdeviceapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7163",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7163/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.device.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7163/signout-callback-oidc",
                        "http://localhost/s2.micronet.device.api/signout-callback-oidc",
                        "https://localhost:7163/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.device.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

				//Employee Configuration
                new Client
                {
                    ClientId = "micronet.employee.api.code",
                    ClientName = "micronet_employee_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetemployeeapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7131",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7131/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.employee.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7131/signout-callback-oidc",
                        "http://localhost/s2.micronet.employee.api/signout-callback-oidc",
                        "https://localhost:7131/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.employee.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //Loan Configuration
                new Client
                {
                    ClientId = "micronet.loan.api.code",
                    ClientName = "micronet_loan_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetloanapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7107",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7107/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.loan.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7107/signout-callback-oidc",
                        "http://localhost/s2.micronet.loan.api/signout-callback-oidc",
                        "https://localhost:7107/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.loan.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //POS Configuration
                new Client
                {
                    ClientId = "micronet.pos.api.code",
                    ClientName = "micronet_pos_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetposapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7008",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7008/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.pod.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7008/signout-callback-oidc",
                        "http://localhost/s2.micronet.pos.api/signout-callback-oidc",
                        "https://localhost:7008/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.pos.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //Product Configuration
                new Client
                {
                    ClientId = "micronet.product.api.code",
                    ClientName = "micronet_product_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetproductapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7121",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7121/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.product.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7121/signout-callback-oidc",
                        "http://localhost/s2.micronet.product.api/signout-callback-oidc",
                        "https://localhost:7121/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.product.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //Reporting Configuration
                new Client
                {
                    ClientId = "micronet.reporting.api.code",
                    ClientName = "micronet_reporting_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetreportingapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7036",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7036/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.reporting.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7036/signout-callback-oidc",
                        "http://localhost/s2.micronet.reporting.api/signout-callback-oidc",
                        "https://localhost:7036/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.reporting.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //Revenue Collection Configuration
                new Client
                {
                    ClientId = "micronet.revenuecollection.api.code",
                    ClientName = "micronet_revenuecollection_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetrevenueapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7022",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7022/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.revenuecollection.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7022/signout-callback-oidc",
                        "http://localhost/s2.micronet.revenuecollection.api/signout-callback-oidc",
                        "https://localhost:7022/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.revenuecollection.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //Session Configuration
                new Client
                {
                    ClientId = "micronet.session.api.code",
                    ClientName = "micronet_session_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetsessionapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7267",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7267/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.session.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7267/signout-callback-oidc",
                        "http://localhost/s2.micronet.session.api/signout-callback-oidc",
                        "https://localhost:7267/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.session.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //Sundry Configuration
                new Client
                {
                    ClientId = "micronet.sundry.api.code",
                    ClientName = "micronet_sundry_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetsundryapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7248",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7248/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.sundry.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7248/signout-callback-oidc",
                        "http://localhost/s2.micronet.sundry.api/signout-callback-oidc",
                        "https://localhost:7248/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.sundry.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //System Configuration
                new Client
                {
                    ClientId = "micronet.system.api.code",
                    ClientName = "micronet_system_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetsystemapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7275",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7275/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.system.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7275/signout-callback-oidc",
                        "http://localhost/s2.micronet.system.api/signout-callback-oidc",
                        "https://localhost:7275/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.system.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //User Configuration
                new Client
                {
                    ClientId = "micronet.user.api.code",
                    ClientName = "micronet_user_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetuserapi",
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7097",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7097/swagger/oauth2-redirect.html",
                        "http://localhost/s2.micronet.user.api/swagger/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7097/signout-callback-oidc",
                        "http://localhost/s2.micronet.user.api/signout-callback-oidc",
                        "https://localhost:7097/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.user.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //Api Gateway Configuration
                new Client
                {
                    ClientId = "micronet.gateway.api.code",
                    ClientName = "micronet_gateway_api_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "micronetaccountapi", "micronetbranchapi", "micronetclientapi",
                        "micronetdeviceapi", "micronetemployeeapi", "micronetloanapi",
                        "micronetposapi", "micronetproductapi", "micronetreportingapi",
                        "micronetrevenueapi", "micronetsessionapi", "micronetsundryapi",
                        "micronetsystemapi", "micronetuserapi"
                    },
                    AllowedCorsOrigins = new []
                    {
                        "https://localhost:7154",
                        "http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7154/signin-oidc",
                        "http://localhost/s2.micronet.gateway.api/signin-oidc",
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7154/signout-callback-oidc",
                        "http://localhost/s2.micronet.gateway.api/signout-callback-oidc",
                        "https://localhost:7154/swagger/signout-callback-oidc",
                        "http://localhost/s2.micronet.gateway.api/swagger/signout-callback-oidc",
                    },
					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				},

                //Admin Configuration
                new Client
                {
                    ClientId = "micronet.ui.code",
                    ClientName = "micronet_ui_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                         "micronetaccountapi", "micronetbranchapi", "micronetclientapi",
                        "micronetdeviceapi", "micronetemployeeapi", "micronetloanapi",
                        "micronetposapi", "micronetproductapi", "micronetreportingapi",
                        "micronetrevenueapi", "micronetsessionapi", "micronetsundryapi",
                        "micronetsystemapi", "micronetuserapi"
                    },
                    AllowedCorsOrigins = new []
                    {
                        //"https://localhost:7192",
						"http://localhost"
                    },
                    RedirectUris = new []
                    {
                        "https://localhost:7192/signin-oidc",
                        //"http://localhost/s2.micronet.admin.ui/signin-oidc",
					},
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:7192",
                        //"https://s2solution.net/s2.micronet.admin.ui",
					},
                    FrontChannelLogoutUri = "https://localhost:7192/signout-oidc",

					// Set token lifetimes
					AccessTokenLifetime = 3600, // 1 hour
					IdentityTokenLifetime = 300, // 5 minutes
					AbsoluteRefreshTokenLifetime = 2592000, // 30 days
					SlidingRefreshTokenLifetime = 1296000, // 15 days
				}
            };
    }
}
