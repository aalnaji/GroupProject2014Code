using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
// new code start:
using System.ServiceModel;

namespace VideoStore.Business.Components.Interfaces
{
    [ServiceContract]
    public interface IEmailProvider
    {
        [OperationContract(IsOneWay=true)]
        void SendMessage(String pAddress, String pMessage);        
    }
}
