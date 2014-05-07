using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBus.Interfaces;
using Microsoft.Practices.ServiceLocation;
using VideoStore.Common;
using MessageBus.Model;

namespace VideoStore.Business.Adapters
{
    public class VideoStoreAdapter : IAdapter
    {
        public void Start()
        {
            ISubscriptionService lSubscriber = ServiceLocator.Current.GetInstance<ISubscriptionService>();
            lSubscriber.Subscribe(typeof(Command).AssemblyQualifiedName, HandleMessage);
        }

        public void HandleMessage(Message pMsg)
        {
            Command lCmd = pMsg as Command;
            lCmd.Execute();
        }
    }
}
