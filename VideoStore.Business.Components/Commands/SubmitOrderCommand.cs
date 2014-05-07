using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBus.Model;
using VideoStore.Business.Entities;
using VideoStore.Common;

namespace VideoStore.Business.Components.Commands
{
    public class SubmitOrderCommand : Command
    {

        public Order Order { get; set; }

        public SubmitOrderCommand(Order pOrder)
        {
            Order = pOrder;
        }

        public override void Execute()
        {
            EntityInsertCommand<Order> lOrderCreateCmd = new EntityInsertCommand<Order>(Order);
            Order.UpdateStockLevels();
            lOrderCreateCmd.Execute();
        }
    }
}
