using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// new code start: added service model
using System.ServiceModel;

// new code start: service contract, operation contract 
namespace Bank.Business.Components.Interfaces
{
    public interface ITransferProvider
    {
        void Transfer(decimal pAmount, int pFromAcctNumber, int pToAcctNumber, String pDescription);
    }
}
