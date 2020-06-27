using OandaBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Services
{
    public interface IPriceStreamerService
    {
        Task<IEnumerable<Price>> StreamPrices(IEnumerable<Stock> stocks);
    }
}
