using Applying.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Extensions
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
                    var context = services.GetRequiredService<ApplyContext>();

                    context.Database.MigrateAsync().Wait();

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
