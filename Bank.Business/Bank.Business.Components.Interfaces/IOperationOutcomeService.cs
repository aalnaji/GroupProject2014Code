using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Business.Entities;

namespace Bank.Business.Components.Interfaces
{
    public interface IOperationOutcomeService
    {
        void NotifyOperationOutcome(OperationOutcome pOutcome); 
    }
}
