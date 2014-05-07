using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBus.Model;
using VideoStore.Business.Entities;

namespace VideoStore.Common
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

        public Command GetEntityInsertCommand<T>(T pEntity) where T : IObjectWithChangeTracker
        {
            EntityInsertCommand<T> lCmd = new EntityInsertCommand<T>(pEntity);
            return lCmd;
        }

        
    }
}
