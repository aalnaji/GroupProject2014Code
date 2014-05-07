using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MessageBus.Interfaces
{
    [ServiceContract]
    public interface ISubscriptionService
    {
        [OperationContract]
        void Subscribe(String pTopic, CallBackDelegate pCallerAddress);

        [OperationContract]
        void Unsubscribe(String pTopic, CallBackDelegate pCallerAddress);
    }
}
