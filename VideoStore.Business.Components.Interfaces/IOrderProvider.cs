using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Entities;

namespace VideoStore.Business.Components.Interfaces
{
    public interface IOrderProvider
    {
        void SubmitOrder(Order pOrder);
        
        // new code start: 
        void ReverseOrder(int pOrderId);
        void PersistOrder(string pMessage, int pOrderId);
    }
}
