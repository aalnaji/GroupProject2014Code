using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Services.Interfaces;
using Bank.Business.Components.Interfaces;
using System.ServiceModel;
using Microsoft.Practices.ServiceLocation;

namespace Bank.Services
{
    public class TransferService : ITransferService
    {
        private ITransferProvider TransferProvider
        {
            get { return ServiceLocator.Current.GetInstance<ITransferProvider>(); }
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void Transfer(decimal pAmount, int pFromAcctNumber, int pToAcctNumber, String pDescription)
        {
            TransferProvider.Transfer(pAmount, pFromAcctNumber, pToAcctNumber, pDescription);
        }
    }
}
