using Blazored.LocalStorage;
using OandaBlazorApp.Models;
using OandaBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OandaBlazorApp.Pages.Product
{
    public class ProductService : IStockService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public ProductService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }
        public async Task<IEnumerable<Models.Stock>> GetStocks(string stockType)
        {
            var accountId = await localStorage.GetItemAsync<string>("api-account-id");
            var instruments = await httpClient.GetFromJsonAsync<Models.Instruments>($"v3/accounts/{accountId}/instruments");
            IEnumerable<Models.Stock> stocks = instruments.instruments;
            if (!string.IsNullOrEmpty(stockType) && stocks != null)
                stocks = stocks.Where(s => s.type == stockType);
            return stocks;
        }
    }
}
