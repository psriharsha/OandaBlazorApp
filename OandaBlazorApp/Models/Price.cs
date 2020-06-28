using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Models
{
    public class Price
    {
        public Side[] bids { get; set; }
        public Side[] asks { get; set; }
        public string instrument { get; set; }
    }

    public class Side
    {
        public string price { get; set; }
        public Int64 liquidity { get; set; }
    }

    public class PriceData
    {
        public Price[] prices { get; set; }
        public string time { get; set; }

    }
}
