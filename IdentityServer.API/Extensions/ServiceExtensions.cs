using IdentityServer.API.Contexts;
using IdentityServer.API.Models;
using IdentityServer.API.Services;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddIdentityServerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
             .AddConfigurationStore(options =>
             {
                 options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
                  sql => sql.MigrationsAssembly(migrationsAssembly));
             })
             .AddOperationalStore(options =>
             {
                 options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
                  sql => sql.MigrationsAssembly(migrationsAssembly));
                 options.EnableTokenCleanup = true;
             })
             .AddAspNetIdentity<ApplicationUser>();

            services.AddScoped<IProfileService, ProfileService>();

            builder.AddDeveloperSigningCredential();

            return services;
        }

        public static IServiceCollection AddExternalAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                });

            return services;
        }
    }
}
