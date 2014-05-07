using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Components.Interfaces;
using System.Diagnostics;

namespace VideoStore.Business.Components
{
    public class EmailProvider : IEmailProvider
    {           
        public void  SendMessage(string pAddress, string pMessage)
        {
            String lPrint = String.Format("{0}: {1}", pAddress, pMessage);
            Console.WriteLine(lPrint);
            Debug.WriteLine(lPrint);
        }
    }
}
