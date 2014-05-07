using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaRevCo.Business.Entities;

namespace MediaRevCo.Business.Components.Interfaces
{
    public interface IReviewSubscriptionProvider
    {
        void SubscribeForReviews(string pAddress, string pUPC);
        void PublishReview(Review pReview);
    }
}
