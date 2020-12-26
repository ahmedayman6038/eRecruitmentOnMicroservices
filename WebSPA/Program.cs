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

namespace WebSPA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("JobsAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:6001/api/");
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

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Oidc", options.ProviderOptions);
                options.UserOptions.RoleClaim = "role";
            })
            .AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

            await builder.Build().RunAsync();
        }
    }
}
