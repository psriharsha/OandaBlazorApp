using OandaBlazorApp.Models;
using OandaBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Pages.Product
{
    public class PriceStreamerService : IPriceStreamerService
    {
        public Task<IEnumerable<Price>> StreamPrices(IEnumerable<Stock> stocks)
        {
            throw new NotImplementedException();
        }
    }
}
