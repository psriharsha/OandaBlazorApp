using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Models
{
    public class Stock
    {
        private Double _bid = new double();
        private string _name;
        public string Name 
        { 
            get
            {
                return _name;
            }
            set
            {
                _name = value.Replace('_', '/');
            }
        }
        public string Currency { get; set; }
        public Double Bid {
            get
            {
                return _bid;
            }
            set
            {
                if (_bid == value)
                    Direction = 0;
                else if (_bid < value)
                    Direction = -1;
                else
                    Direction = 1;
                if (_bid != value)
                {
                    _bid = value;
                }
            }
        }
        public Double Ask { get; set; }
        public string Type { get; set; }
        public int Direction { get; set; } = 0;
        public bool IsSelected { get; set; }
        public bool IsHidden { get; set; }
    }
}
