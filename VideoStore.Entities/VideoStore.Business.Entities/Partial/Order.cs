using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoStore.Business.Entities
{
    public partial class Order
    {
        public decimal Total
        {
            get
            {
                decimal lTotal = 0.0M;
                foreach (OrderItem lItem in this.OrderItems)
                {
                    lTotal = lTotal + lItem.Media.Price;
                }
                return lTotal;
            }
        }

        public void UpdateStockLevels()
        {
            foreach (OrderItem lItem in this.OrderItems)
            {
                if (lItem.Media.Stocks.Quantity - lItem.Quantity >= 0)
                {
                    lItem.Media.Stocks.Quantity -= lItem.Quantity;
                }
                else
                {
                    throw new Exception("Cannot place an order - no more stock for media item");
                }
            }
        }
    }
}
