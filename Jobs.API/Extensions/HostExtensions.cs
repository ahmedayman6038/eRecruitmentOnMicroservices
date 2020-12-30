using Jobs.API.Infrastructure.Contexts;
using Jobs.API.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Extensions
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
                    var context = services.GetRequiredService<JobContext>();

                    context.Database.MigrateAsync().Wait();
                    DefaultCountries.SeedAsync(context).Wait();

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
