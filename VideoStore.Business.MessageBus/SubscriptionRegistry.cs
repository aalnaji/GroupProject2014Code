using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MessageBus
{
    /// <summary>
    /// Keeps track of subscriptions. NOT THREAD SAFE!!!
    /// </summary>
    public class SubscriptionRegistry
    {

        private static SubscriptionRegistry sRegistry;

        public static SubscriptionRegistry Instance
        {
            get
            {
                //not thread safe!
                if (sRegistry == null)
                {
                    sRegistry = new SubscriptionRegistry();
                }
                return sRegistry;
            }
        }

        private static Dictionary<String, List<CallBackDelegate>> sTopicSubscriptions =
            new Dictionary<string, List<CallBackDelegate>>();

        public SubscriptionRegistry()
        {
        }

        public void AddSubscription(String pTopic, CallBackDelegate pCallBack)
        {
            if (!sTopicSubscriptions.ContainsKey(pTopic))
            {
                sTopicSubscriptions.Add(pTopic, new List<CallBackDelegate>());
            }
            if (!sTopicSubscriptions[pTopic].Contains(pCallBack))
            {
                sTopicSubscriptions[pTopic].Add(pCallBack);
            }
        }



        public void RemoveSubscription(String pTopic, CallBackDelegate pHandlerAddress)
        {
            if (sTopicSubscriptions.ContainsKey(pTopic))
            {
                if (sTopicSubscriptions[pTopic].Contains(pHandlerAddress))
                {
                    sTopicSubscriptions[pTopic].Remove(pHandlerAddress);
                }
            }
        }

        public List<CallBackDelegate> GetTopicSubscribers(String pTopic)
        {
            return sTopicSubscriptions.ContainsKey(pTopic) ? sTopicSubscriptions[pTopic] : new List<CallBackDelegate>();
        }

    }
}
