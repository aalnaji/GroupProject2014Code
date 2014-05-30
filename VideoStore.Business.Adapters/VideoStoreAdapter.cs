using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBus.Interfaces;
using Microsoft.Practices.ServiceLocation;
using VideoStore.Common;
using MessageBus.Model;
using VideoStore.Business.Components.Commands;

// new code start: added references to recive the transfer response
using System.ServiceModel;
using Bank.Business.Entities;
using Bank.Business.Components.Interfaces;
// to persist or reverse orders
using VideoStore.Business.Components.Interfaces;

namespace VideoStore.Business.Adapters
{
    public class VideoStoreAdapter : IAdapter, IOperationOutcomeService
    {
        // new code start:
        private const String cResponseAddress = "net.msmq://localhost/private/Responseq";
        
        public void Start()
        {
            ISubscriptionService lSubscriber = ServiceLocator.Current.GetInstance<ISubscriptionService>();
            lSubscriber.Subscribe(typeof(Command).AssemblyQualifiedName, HandleMessage);
            // new code start:
            HostBankOutcomeService();
        }

        public void HandleMessage(Message pMsg)
        {
            Command lCmd = pMsg as Command;
            lCmd.Execute();
        }

        // new code start:
        private void HostBankOutcomeService()
        {
            ServiceHost lHost = new ServiceHost(typeof(VideoStoreAdapter));
            lHost.AddServiceEndpoint(typeof(IOperationOutcomeService), new NetMsmqBinding(NetMsmqSecurityMode.None),
               cResponseAddress);
            lHost.Open();
        }

        // new code start: h
        public void NotifyOperationOutcome(Bank.Business.Entities.OperationOutcome pOutcome)
        {
            String message, email, bankResponseMessage;
            int orderId;
            bankResponseMessage = pOutcome.Message;

            char[] spilitChars = { ',' };
            string[] splitMessage = bankResponseMessage.Split(spilitChars);

            message = splitMessage[0];
            orderId = Convert.ToInt32(splitMessage[1]);
            email = splitMessage[2];

            if (message.Equals("TransferSuccessful"))
            {
                //transfer failed
                // ServiceLocator.Current.GetInstance<IOrderProvider>().
                Console.WriteLine("sucess");

            }
            else
            {
                Console.WriteLine("fail");
                ServiceLocator.Current.GetInstance<IOrderProvider>().ReverseOrder(orderId);
            }

            ServiceLocator.Current.GetInstance<IOrderProvider>().PersistOrder(message, orderId);
            ServiceLocator.Current.GetInstance<IEmailProvider>().SendMessage(email, splitMessage[0]);

        }
    }
}
