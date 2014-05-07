using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockTicker.Interfaces;
using System.Collections.ObjectModel;
using StockTicker.Model;

namespace StockTicker.ViewModels
{
    public class TickerViewModel : ITickerViewModel
    {
        public ObservableCollection<TradeItem> TradeItems { get; set; }

        public TickerViewModel()
        {
            TradeItems = new ObservableCollection<TradeItem>();
        }

        

        public void AcceptTradeItemUpdate(TradeItem pItem)
        {
            TradeItem lItem = TradeItems.Where((pIt) => pIt.Name == pItem.Name).FirstOrDefault();
            if (lItem != null)
            {
                TradeItems.Remove(lItem);
            }
            TradeItems.Add(pItem);
        }


    }
}
