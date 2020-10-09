using Blazored.LocalStorage;
using BlazorWidget;
using Microsoft.AspNetCore.Components;
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
        private readonly IWidgetService widgetService;
        private readonly NavigationManager navigationManager;
        public event EventHandler<string> OnChildClosed;
        public event EventHandler OnPricesChanged;

        private List<Stock> StocksList = new List<Stock>();

        public ProductService(HttpClient httpClient, ILocalStorageService localStorage, 
            IPriceStreamerService priceService, IWidgetService widgetService, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.priceService = priceService;
            this.widgetService = widgetService;
            this.navigationManager = navigationManager;
            Timer timer = new Timer();
            timer.Interval = 2000;
            timer.Elapsed += PollPrices;
            timer.AutoReset = false;
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
            if (StocksList.Count != 0)
                return StocksList;
            var accountId = await localStorage.GetItemAsync<string>("api-account-id");
            var instruments = await httpClient.GetFromJsonAsync<Models.Instruments>($"v3/accounts/{accountId}/instruments");
            IEnumerable<Models.Stock> stocks = instruments.instruments;
            if (!string.IsNullOrEmpty(stockType) && stocks != null)
                stocks = stocks.Where(s => s.type == stockType);
            StocksList = stocks.Take(10).ToList();
            await localStorage.SetItemAsync<List<Stock>>("BaseStocks", stocks.ToList());
            return stocks;
        }

        public async Task PopOutStock(Stock stock)
        {
            await widgetService.Open($"{navigationManager.BaseUri}/stock?stock={stock.name}", stock.name, 150, 335);
            widgetService.OnWidgetClosed += OnWidgetClosed;
        }

        public void OnWidgetClosed(object sender, string e)
        {
            OnChildClosed?.Invoke(this, e);
        }
    }
}
