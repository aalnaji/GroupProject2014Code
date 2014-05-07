using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace VideoStore.Business.Components.Interfaces
{
    public interface IEmailProvider
    {
        void SendMessage(String pAddress, String pMessage);        
    }
}
