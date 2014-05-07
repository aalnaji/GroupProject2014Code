using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank.Business.Components.Interfaces
{
    public interface ITransferProvider
    {
        void Transfer(decimal pAmount, int pFromAcctNumber, int pToAcctNumber, String pDescription);
    }
}
