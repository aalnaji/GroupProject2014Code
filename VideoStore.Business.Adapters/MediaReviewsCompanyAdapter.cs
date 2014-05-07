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
            lHost.Open();
        }

        public void HandleMessage(Message pMsg)
        {
            if (pMsg.GetType() == typeof(EntityInsertCommand<Media>))
            {
                EntityInsertCommand<Media> lCmd = pMsg as EntityInsertCommand<Media>;
                ReviewSubscriptionServiceClient lClient = new ReviewSubscriptionServiceClient();
                lClient.SubscribeForReviews(cSubscriberServiceAddress, lCmd.Entity.UPC);
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
