using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common;
using MessageBus.Model;
using MessageBus.Interfaces;
using System.Transactions;

namespace MessageBus
{
    public class PublisherService : IPublisherService
    {
        public void Publish(Message pMessage)
        {
            using (TransactionScope lScope = new TransactionScope())
            {
                foreach (CallBackDelegate lHandlerAddress in SubscriptionRegistry.Instance.GetTopicSubscribers(pMessage.Topic))
                {
                    lHandlerAddress(pMessage);
                }
                lScope.Complete();
            }
        }
    }
}
