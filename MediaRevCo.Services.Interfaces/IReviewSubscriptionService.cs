using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MediaRevCo.Services.Interfaces
{
    [ServiceContract]
    public interface IReviewSubscriptionService
    {
        [OperationContract(IsOneWay=true)]
        void SubscribeForReviews(String pAddress, String pUPC);
    }
}
