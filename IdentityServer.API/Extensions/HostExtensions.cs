using IdentityServer.API.Configuration;
using IdentityServer.API.Contexts;
using IdentityServer.API.Models;
using IdentityServer.API.Seeds;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateAndSeedDb(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
                    var persistedGrantDbContext = services.GetRequiredService<PersistedGrantDbContext>();
                    var configurationDbContext = services.GetRequiredService<ConfigurationDbContext>();
                    var urlConfig = services.GetRequiredService<IOptions<UrlsConfig>>().Value;

                    applicationDbContext.Database.MigrateAsync().Wait();
                    persistedGrantDbContext.Database.MigrateAsync().Wait();
                    configurationDbContext.Database.MigrateAsync().Wait();

                    DefaultRoles.SeedAsync(roleManager).Wait();
                    DefaultSuperAdmin.SeedAsync(userManager).Wait();
                    DefaultBasicUser.SeedAsync(userManager).Wait();
                    IdentityServerConfiguration.SeedAsync(configurationDbContext, urlConfig).Wait();

                    Log.Information("Finished Database Migration And Seeding");
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "An error occurred while seeding and migrate the DB");
                }
            }
            return webHost;
        }
    }
}
