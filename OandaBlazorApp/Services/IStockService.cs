using OandaBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Services
{
    public interface IStockService
    {
        event EventHandler OnPricesChanged;
        Task<IEnumerable<Stock>> GetStocks(string stockType);
    }
}
