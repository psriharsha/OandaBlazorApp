using Microsoft.AspNetCore.Components;
using OandaBlazorApp.Models;
using OandaBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Pages.Home
{
    public class HomeBase : ComponentBase
    {
        public IEnumerable<Models.Stock> StocksList { get; set; }
        [Inject]
        public IStockService StockService { get; set; }
        public ViewType viewType { get; set; } = ViewType.TABLE;

        protected override async Task OnInitializedAsync()
        {
            StocksList = await StockService.GetStocks("CURRENCY");
        }
    }
}
