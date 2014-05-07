using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Entities;
using MessageBus.Model;
using VideoStore.Business.Components.Commands;

namespace VideoStore.Business.Components
{
    public class CommandFactory
    {
        private static CommandFactory sFactory = new CommandFactory();
        public static CommandFactory Instance
        {
            get
            {
                return sFactory;
            }
        }

        public Command GetSubmitOrderCommand(Order pOrder)
        {
            return new SubmitOrderCommand(pOrder);
        }
    }
}
