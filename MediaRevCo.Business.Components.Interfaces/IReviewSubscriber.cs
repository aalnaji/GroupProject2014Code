using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaRevCo.Business.Entities;
using System.ServiceModel;

namespace MediaRevCo.Business.Components.Interfaces
{
    [ServiceContract]
    public interface IReviewSubscriber
    {
        [OperationContract(IsOneWay=true)]
        void ReceiveReview(Review pReview);
    }
}
