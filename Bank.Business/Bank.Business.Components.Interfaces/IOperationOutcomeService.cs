using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Business.Entities;
// new code start: added service model
using System.ServiceModel;

// new code start: service contract, operation contract 
namespace Bank.Business.Components.Interfaces
{
    [ServiceContract]
    public interface IOperationOutcomeService
    {
        [OperationContract(IsOneWay=true)]
        void NotifyOperationOutcome(OperationOutcome pOutcome); 
    }
}
