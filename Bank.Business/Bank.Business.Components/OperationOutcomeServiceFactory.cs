using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Bank.Business.Components.Interfaces;

namespace Bank.Business.Components
{
    public class OperationOutcomeServiceFactory
    {
        public static IOperationOutcomeService GetOperationOutcomeService(String pAddress)
        {
            // new code start: changed from net.tcp to msmq
            ChannelFactory<IOperationOutcomeService> lChannelFactory = new ChannelFactory<IOperationOutcomeService>(new NetMsmqBinding(NetMsmqSecurityMode.None), new EndpointAddress(pAddress));
            return lChannelFactory.CreateChannel();
        }
    }
}
