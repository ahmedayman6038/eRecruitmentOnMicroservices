using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Configuration
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiResource> GetApiResources() =>
          new ApiResource[]
          {
              new ApiResource("jobs", "Jobs API")
              {
                  Scopes = { "jobs.read", "jobs.write", "jobs.post", "manage" }
              },
              new ApiResource("applying", "Applying API")
              {
                  Scopes = { "applying.read", "applying.write", "manage" }
              },
              new ApiResource("gatewayagg", "Gateway Agg")
              {
                  Scopes = { "agg.stat", "manage" }
              }
          };

        public static IEnumerable<ApiScope> GetApiScopes() =>
           new ApiScope[]
           {
                new ApiScope("jobs.read", "Read data from jobs service"),
                new ApiScope("jobs.write", "Write data to jobs service"),
                new ApiScope("jobs.post", "Post jobs to jobs service"),
                new ApiScope("applying.read", "Read data from applying service"),
                new ApiScope("applying.write", "Write data to applying service"),
                new ApiScope("agg.stat", "Provides access to statistics services"),
                new ApiScope("manage", "Provides administrative access to all services")
           };      

        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl) =>
            new Client[]
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientUri = $"{clientsUrl["Mvc"]}",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>
                    {
                        $"{clientsUrl["Mvc"]}/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{clientsUrl["Mvc"]}/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "jobs.read",
                        "jobs.write",
                        "jobs.post",
                        "applying.read",
                        "applying.write",
                        "agg.stat",
                        "manage"
                    }
                },
                new Client
                {
                    ClientId = "jobsswaggerui",
                    ClientName = "Jobs Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{clientsUrl["JobsApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientsUrl["JobsApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "jobs.read",
                        "jobs.write",
                        "jobs.post",
                        "manage"
                    }
                },
                new Client
                {
                    ClientId = "applyingswaggerui",
                    ClientName = "Applying Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"{clientsUrl["ApplyingApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientsUrl["ApplyingApi"]}/swagger/" },
                    AllowedScopes =
                    {
                        "applying.read",
                        "applying.write",
                        "manage"
                    }
                },
                new Client
                {
                    ClientId = "apigatewayswaggerui",
                    ClientName = "Api gateway Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"{clientsUrl["ApiGateway"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientsUrl["ApiGateway"]}/swagger/" },
                    AllowedScopes =
                    {

                        "manage",
                        "agg.stat"
                    }
                },
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    RequireConsent = true,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris =           { $"{clientsUrl["Mvc"]}/Home/Callback" },
                    PostLogoutRedirectUris = { $"{clientsUrl["Mvc"]}/Home/Charts" },
                    AllowedCorsOrigins =     { $"{clientsUrl["Mvc"]}" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "jobs.read",
                        "jobs.write",
                        "jobs.post",
                        "applying.read",
                        "applying.write",
                        "agg.stat",
                        "manage"
                    }
                },
                new Client
                {
                    ClientId = "blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { $"{clientsUrl["Spa"]}" },
                    RedirectUris = { $"{clientsUrl["Spa"]}/authentication/login-callback" },
                    PostLogoutRedirectUris = { $"{clientsUrl["Spa"]}/authentication/logout-callback" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "jobs.read",
                        "jobs.write",
                        "jobs.post",
                        "applying.read",
                        "applying.write",
                        "agg.stat",
                        "manage"
                    }
                }
            };
    }
}
