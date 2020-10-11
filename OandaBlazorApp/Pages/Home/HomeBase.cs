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
        public ILocalStorageService localStorage { get; set; }
        public ViewType viewType { get; set; } = ViewType.TABLE;

        protected string StatusMessage;

        protected override async Task OnInitializedAsync()
        {
            StocksList = (await StockService.GetStocks("CURRENCY")).Take(10);
            StockService.OnPricesChanged += RefreshView;
            StockService.OnChildClosed += StockService_OnChildClosed;

            ViewType type = await localStorage.GetItemAsync<ViewType>("ViewType");
            await ChangeViewType(type);
        }

        private void StockService_OnChildClosed(object sender, string e)
        {
            StatusMessage = $"{e} has been closed";
            StateHasChanged();
        }

        private void RefreshView(object sender, EventArgs e)
        {
            StateHasChanged();
        }

        protected async Task ChangeViewType(ViewType type)
        {
            viewType = type;
            await localStorage.SetItemAsync<ViewType>("ViewType", type);
        }
    }
}
