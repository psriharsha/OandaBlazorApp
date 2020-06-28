using Blazored.LocalStorage;
using OandaBlazorApp.Models;
using OandaBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OandaBlazorApp.Pages.Product
{
    public class PriceStreamerService : IPriceStreamerService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public PriceStreamerService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }
        public async Task StreamPrices(IEnumerable<Stock> stocks)
        {
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                IEnumerable<string> stockNames = stocks.Select(s => s.name);
                foreach(string name in stockNames)
                {
                    queryBuilder.Append(name);
                    queryBuilder.Append(",");
                }
                string query = (queryBuilder.ToString()).Substring(0, queryBuilder.ToString().Length - 1);
                var accountId = await localStorage.GetItemAsync<string>("api-account-id");
                PriceData priceData = await httpClient.GetFromJsonAsync<PriceData>($"v3/accounts/{accountId}/pricing?instruments={query}");
                foreach(var price in priceData.prices)
                {
                    Stock thisStock = stocks.Where(s => s.name.Equals(price.instrument)).FirstOrDefault();
                    if (null != thisStock)
                    {
                        thisStock.bid = Double.Parse(price.bids[0].price);
                        thisStock.ask = Double.Parse(price.asks[0].price);
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
