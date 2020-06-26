using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Misc
{
    public static class StockNameExtension
    {
        public static string DisplayStockName(this string stockName)
        {
            return stockName.Replace('_', '/');
        }
    }
}
