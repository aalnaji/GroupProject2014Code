using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MessageBus.Interfaces;
using MessageBus.Model;

namespace MessageBus
{
    public delegate void CallBackDelegate(Message pMsg);
    public class SubscriptionService : ISubscriptionService
    {
        public void Subscribe(string pTopic, CallBackDelegate pCallBack)
        {
            Console.WriteLine("Subscription for " + pTopic + " received");
            SubscriptionRegistry.Instance.AddSubscription(pTopic, pCallBack);
        }

        public void Unsubscribe(string pTopic, CallBackDelegate pCaller)
        {
            SubscriptionRegistry.Instance.RemoveSubscription(pTopic, pCaller);
        }
    }
}
