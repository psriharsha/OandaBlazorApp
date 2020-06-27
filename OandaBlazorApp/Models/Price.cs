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
    }

    public class Side
    {
        public double price { get; set; }
        public Int64 liquidity { get; set; }
    }
}
