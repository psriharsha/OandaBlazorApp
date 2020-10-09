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
using System.Timers;

namespace OandaBlazorApp.Pages.Product
{
    public class ProductService : IStockService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly IPriceStreamerService priceService;

        private List<Stock> StocksList = new List<Stock>();
        public event EventHandler OnPricesChanged;

        public ProductService(HttpClient httpClient, ILocalStorageService localStorage, IPriceStreamerService priceService)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.priceService = priceService;
            Timer timer = new Timer();
            timer.Interval = 2000;
            timer.Elapsed += PollPrices;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void PollPrices(object sender, ElapsedEventArgs e)
        {
            if (StocksList.Count > 9)
            priceService.StreamPrices(StocksList.Take(10));
            OnPricesChanged?.Invoke(this, e);
        }

        public async Task<IEnumerable<Models.Stock>> GetStocks(string stockType)
        {
            var accountId = await localStorage.GetItemAsync<string>("api-account-id");
            var instruments = await httpClient.GetFromJsonAsync<Models.Instruments>($"v3/accounts/{accountId}/instruments");
            IEnumerable<Models.Stock> stocks = instruments.instruments;
            if (!string.IsNullOrEmpty(stockType) && stocks != null)
                stocks = stocks.Where(s => s.type == stockType);
            StocksList = stocks.ToList();
            await localStorage.SetItemAsync<List<Stock>>("BaseStocks", stocks.ToList());
            return stocks;
        }
    }
}
