using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using StockTicker.ViewModels;
using StockTicker.Model;
using Common.Model;

namespace StockTicker.Transformations
{
    public class PriceChangeMessageToTradeItem : IVisitor
    {
        public TradeItem Result { get; set; }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is PriceChangeMessage)
            {
                PriceChangeMessage lMsg = pVisitable as PriceChangeMessage;
                Result = new TradeItem() { Change = lMsg.Change, Name = lMsg.Item, Price = lMsg.Price };
            }
        }
    }
}
