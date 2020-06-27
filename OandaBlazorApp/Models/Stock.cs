using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OandaBlazorApp.Models
{
    public class Stock
    {
        private Double _bid = new double();
        public string name 
        {
            get; set;
        }
        public string currency { get; set; }
        public Double bid {
            get
            {
                return _bid;
            }
            set
            {
                if (_bid == value)
                    direction = 0;
                else if (_bid < value)
                    direction = -1;
                else
                    direction = 1;
                if (_bid != value)
                {
                    _bid = value;
                }
            }
        }
        public Double ask { get; set; }
        public string type { get; set; }
        public int direction { get; set; } = 0;
        public bool isSelected { get; set; }
        public bool isHidden { get; set; }
    }

    public class Instruments
    {
        public Stock[] instruments { get; set; }
    }

}
