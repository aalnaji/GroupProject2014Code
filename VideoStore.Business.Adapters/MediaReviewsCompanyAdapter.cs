using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBus.Interfaces;
using Microsoft.Practices.ServiceLocation;
using MessageBus.Model;
using VideoStore.Common;
using VideoStore.Business.Entities;
using MediaRevCo.Business.Components.Interfaces;
using VideoStore.Business.Adapters.Transformations;
using MediaRevCo.Services.Interfaces;
using VideoStore.Business.Adapters.ReviewSubscriptionService;
using VideoStore.Business.Adapters;
using System.ServiceModel;
using Common;

namespace VideoStore.Business.Adapters
{
    public class MediaReviewsCompanyAdapter : IAdapter, IReviewSubscriber
    {

        private const String cSubscriberServiceAddress = "net.tcp://localhost:9010/SubscriberService";

        public void Start()
        {
            ISubscriptionService lSubscriber = ServiceLocator.Current.GetInstance<ISubscriptionService>();
            lSubscriber.Subscribe(typeof(Command).AssemblyQualifiedName, HandleMessage);
            HostReviewSubscriberService();
        }

        private void HostReviewSubscriberService()
        {
            ServiceHost lHost = new ServiceHost(typeof(MediaReviewsCompanyAdapter));

            lHost.AddServiceEndpoint(typeof(IReviewSubscriber), new NetTcpBinding(),
               cSubscriberServiceAddress);

            //Code start - new
            // Add MSMQ service end point to the service
            NetMsmqBinding binding = new NetMsmqBinding(NetMsmqSecurityMode.None)
            {
                ExactlyOnce = false
            };

            lHost.AddServiceEndpoint(typeof(IReviewSubscriber), binding,
               "net.msmq://localhost/private/ReviewQ");

            //Create private queue if not exists
            string queueName = @".\private$\ReviewQ";
            if (!System.Messaging.MessageQueue.Exists(queueName))
                System.Messaging.MessageQueue.Create(queueName, false);
            //Code end - new

            lHost.Open();
        }

        public void HandleMessage(Message pMsg)
        {
            if (pMsg.GetType() == typeof(EntityInsertCommand<Media>))
            {
                EntityInsertCommand<Media> lCmd = pMsg as EntityInsertCommand<Media>;
                //Code start - new
                // Commented the below line of code as currently the end points are not TCP
                //ReviewSubscriptionServiceClient lClient = new ReviewSubscriptionServiceClient();
                //lClient.SubscribeForReviews(cSubscriberServiceAddress, lCmd.Entity.UPC);
                
                //Added the below line of code to connect to the review subscription service using MSMQ 
                // This will be subscribed only during video catalogue creation so will not called everytime until video gets added
                MediaRevCo.Services.Interfaces.IReviewSubscriptionService lSubscriber = ServiceFactory.GetService<MediaRevCo.Services.Interfaces.IReviewSubscriptionService>("net.msmq://localhost/private/SubscribeQ");
                lSubscriber.SubscribeForReviews(cSubscriberServiceAddress, lCmd.Entity.UPC);
                //Code end - new
            }

        }

        public void ReceiveReview(MediaRevCo.Business.Entities.Review pReview)
        {
            ReviewTransformVisitor lVis = new ReviewTransformVisitor();
            pReview.Accept(lVis);
            ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                CommandFactory.Instance.GetEntityInsertCommand<VideoStore.Business.Entities.Review>(lVis.Result)
            );
        }
    }
}
