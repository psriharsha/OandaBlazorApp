using Microsoft.AspNetCore.Components;
using OandaBlazorApp.Models;
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

        protected void PlaceOrder(Boolean isBuy)
        {

        }
    }
}
