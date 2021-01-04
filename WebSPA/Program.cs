using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebSPA.Security;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using WebSPA.Interfaces;
using WebSPA.Services;

namespace WebSPA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("JobsAPI", (sp,client) =>
            {
                client.BaseAddress = new Uri("https://localhost:6001/api/");
                client.EnableIntercept(sp);
            })
            .AddHttpMessageHandler(sp =>
            {
                var handler = sp.GetService<AuthorizationMessageHandler>()
                .ConfigureHandler(
                    authorizedUrls: new[] { "https://localhost:6001" }
                    //scopes: new[] { "manage" }
                );
                return handler;
            });

            builder.Services.AddScoped(
                sp => sp.GetService<IHttpClientFactory>().CreateClient("JobsAPI"));

            builder.Services.AddHttpClientInterceptor();

            builder.Services.AddScoped<HttpInterceptorService>();

            builder.Services.AddScoped<IJobService, JobService>();

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Oidc", options.ProviderOptions);
                options.UserOptions.RoleClaim = "role";
            })
            .AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

            builder.Services.AddAntDesign();

            await builder.Build().RunAsync();
        }
    }
}
