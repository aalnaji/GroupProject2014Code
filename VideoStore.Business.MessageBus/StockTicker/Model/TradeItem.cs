using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockTicker.Model
{
    public class TradeItem
    {
        public String Name { get; set; }
        public double Price { get; set; }
        public double Change { get; set; }
    }
}
