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
using OandaBlazorApp.Pages.Product;

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

            var host = builder.Build();
            var localStorage = host.Services.GetRequiredService<ISyncLocalStorageService>();
            var token = localStorage.GetItem<string>("api-token");

            builder.Services.AddHttpClient<ILoginService, LoginService>(client =>
            {
                client.BaseAddress = new Uri("https://api-fxpractice.oanda.com");
            });
            builder.Services.AddHttpClient<IStockService, ProductService>(client =>
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                client.BaseAddress = new Uri("https://api-fxpractice.oanda.com");
            });
            builder.Services.AddHttpClient<IPriceStreamerService, PriceStreamerService>(client =>
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                client.BaseAddress = new Uri("https://stream-fxpractice.oanda.com");
            });

            await builder.Build().RunAsync();
        }
    }
}
