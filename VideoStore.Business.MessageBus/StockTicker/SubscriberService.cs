using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Model;
using StockTicker.Transformations;

namespace StockTicker
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Common.Model.Message pMessage)
        {
            if (pMessage is PriceChangeMessage)
            {
                PriceChangeMessage lMessage = pMessage as PriceChangeMessage;
                PriceChangeMessageToTradeItem lVisitor = new PriceChangeMessageToTradeItem();
                pMessage.Accept(lVisitor);
                MainWindow.sViewModel.AcceptTradeItemUpdate(lVisitor.Result);
            }
        }
    }
}
