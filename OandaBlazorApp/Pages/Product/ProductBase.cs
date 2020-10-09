using Blazored.LocalStorage;
using BlazorWidget;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using OandaBlazorApp.Models;
using OandaBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Pages.Product
{
    public class ProductBase : ComponentBase
    {
        [Parameter]
        public OandaBlazorApp.Models.Stock stock { get; set; }
        [Parameter]
        public ViewType type { get; set; }
        [Parameter]
        public EventCallback Order { get; set; }
        [Parameter]
        public Boolean IsInContainer { get; set; } = true;
        [Inject]
        public IWidgetService WidgetService { get; set; }
        [Inject]
        public NavigationManager navManager { get; set; }
        [Inject]
        public IStockService stockService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (stock == null)
            {
                var uri = navManager.ToAbsoluteUri(navManager.Uri);
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("stock", out var stockName))
                {
                    var stocks = (await stockService.GetStocks("CURRENCY")).ToList();
                    stock = stocks.Find(s => s.name.Equals(stockName, StringComparison.OrdinalIgnoreCase));
                    stockService.OnPricesChanged += RefreshCard;
                    
                    type = ViewType.CARD;
                }
            }
        }

        private void RefreshCard(object sender, EventArgs e)
        {
            StateHasChanged();
        }

        protected void PlaceOrder(Boolean isBuy)
        {

        }

        protected void Extract(bool isOut)
        {
            WidgetService.Open($"http://localhost:50514/stock?stock={stock.name}", stock.name, 205, 216);
        }
    }
}
