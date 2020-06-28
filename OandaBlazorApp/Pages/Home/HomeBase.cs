using Microsoft.AspNetCore.Components;
using OandaBlazorApp.Models;
using OandaBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;
using System.Linq;
using Blazored.LocalStorage;

namespace OandaBlazorApp.Pages.Home
{
    public class HomeBase : ComponentBase
    {
        public IEnumerable<Models.Stock> StocksList { get; set; }
        [Inject]
        public IStockService StockService { get; set; }
        [Inject]
        public IPriceStreamerService priceStreamerService { get; set; }
        [Inject]
        public ILocalStorageService localStorage { get; set; }
        public ViewType viewType { get; set; } = ViewType.TABLE;

        protected override async Task OnInitializedAsync()
        {
            StocksList = (await StockService.GetStocks("CURRENCY")).Take(10);
            Timer timer = new Timer();
            timer.Interval = 2000;
            timer.Elapsed += PollPrices;
            timer.AutoReset = false;
            timer.Enabled = true;

            ViewType type = await localStorage.GetItemAsync<ViewType>("ViewType");
            await ChangeViewType(type);
        }

        private async void PollPrices(object sender, ElapsedEventArgs e)
        {
            if (null != StocksList)
            {
                await priceStreamerService.StreamPrices(StocksList);
                StateHasChanged();
            }
        }

        protected async Task ChangeViewType(ViewType type)
        {
            viewType = type;
            await localStorage.SetItemAsync<ViewType>("ViewType", type);
        }
    }
}
