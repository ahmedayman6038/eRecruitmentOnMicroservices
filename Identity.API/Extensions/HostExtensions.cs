using Identity.API.Contexts;
using Identity.API.Models;
using Identity.API.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Extensions
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
                    var context = services.GetRequiredService<IdentityContext>();

                    context.Database.MigrateAsync().Wait();
                    DefaultRoles.SeedAsync(roleManager).Wait();
                    DefaultSuperAdmin.SeedAsync(userManager).Wait();
                    DefaultBasicUser.SeedAsync(userManager).Wait();

                    Log.Information("Finished Database Migration And Seeding");
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "An error occurred while seeding and migrate the DB");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
            return webHost;
        }
    }
}
