using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaRevCo.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using MediaRevCo.Business.Components.Interfaces;

namespace MediaRevCo.Services
{
    public class ReviewSubscriptionService : IReviewSubscriptionService
    {
        public void SubscribeForReviews(string pAddress, string pUPC)
        {
            ServiceLocator.Current.GetInstance<IReviewSubscriptionProvider>().
                SubscribeForReviews(pAddress, pUPC);
        }
    }
}
