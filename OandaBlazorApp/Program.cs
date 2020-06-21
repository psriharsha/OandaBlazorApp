using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OandaBlazorApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using OandaBlazorApp.Pages.Login;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace OandaBlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddOptions();
            builder.Services.AddAuthenticationCore();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddHttpClient<ILoginService, LoginService>(client =>
            {
                client.BaseAddress = new Uri("https://api-fxpractice.oanda.com/v3/accounts");
            });

            await builder.Build().RunAsync();
        }
    }
}
