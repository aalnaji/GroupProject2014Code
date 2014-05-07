using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBus.Model;
using VideoStore.Common;
using VideoStore.Business.Entities;
using MessageBus.Interfaces;
using Microsoft.Practices.ServiceLocation;
using VideoStore.Business.Adapters.TransferService;
using VideoStore.Business.Components.Commands;

namespace VideoStore.Business.Adapters
{
    public class BankAdapter :IAdapter
    {
        public void Start()
        {
            ISubscriptionService lSubscriber = ServiceLocator.Current.GetInstance<ISubscriptionService>();
            lSubscriber.Subscribe(typeof(Command).AssemblyQualifiedName, HandleMessage);
        }

        public void HandleMessage(Message pMsg)
        {
            if (pMsg.GetType() == typeof(SubmitOrderCommand))
            {
                SubmitOrderCommand lCmd = pMsg as SubmitOrderCommand;
                Order lOrder = lCmd.Order;
                TransferServiceClient lClient = new TransferServiceClient();
                lClient.Transfer(lOrder.Total, lOrder.Customer.BankAccountNumber, GetStoreAcctNumber(), "orderPurchase");
            }
        }

        private int GetStoreAcctNumber()
        {
            return 123;
        }
    }
}
