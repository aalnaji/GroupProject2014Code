using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBus.Model;
using System.Transactions;
using VideoStore.Business.Entities;

namespace VideoStore.Common
{
    public class EntityInsertCommand<T> : Command where T : IObjectWithChangeTracker
    {
        T mEntity;

        public T Entity { get { return mEntity; } }

        public EntityInsertCommand(T pEntity)
        {
            mEntity = pEntity;
        }

        public override void Execute()
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                lContainer.ApplyChanges<T>(mEntity.GetType().Name + "s", mEntity);
                lContainer.SaveChanges();
                lScope.Complete();
            }
        }
    }
}
