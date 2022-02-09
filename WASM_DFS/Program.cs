using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WASM_DFS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevCorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddOidcAuthentication(options =>
            {
                //// options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                // options.ProviderOptions.Authority = "https://sl-kl-srv05.creativesoftware.com/dips.oauth";
                // options.ProviderOptions.ClientId = "dips-anc-diagnosticmanager";
                // //options.ProviderOptions.ClientSecret = idpConfigs.Secret;
                // options.ProviderOptions.ResponseType = "code";
                // options.ProviderOptions.DefaultScopes.Add("openid");
                // options.ProviderOptions.DefaultScopes.Add("profile");
                // options.ProviderOptions.DefaultScopes.Add("offline_access");
                // options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:44322/authentication/logout-callback";
                // options.ProviderOptions.RedirectUri = "https://localhost:44322/authentication/login-callback";

                // // options.ProviderOptions.SignedOutCallbackPath = idpConfigs.LogoutRedirectUrl;
                // //options.ProviderOptions.save = true;
                // //options.ProviderOptions.GetClaimsFromUserInfoEndpoint = true;

                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
