using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Entities;
using System.Transactions;
using VideoStore.Common;
using Microsoft.Practices.ServiceLocation;
using MessageBus.Interfaces;

namespace VideoStore.Business.Components
{
    public class OrderProvider : IOrderProvider
    {
        private IEmailProvider EmailProvider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IEmailProvider>();
            }
        }

        public void SubmitOrder(Entities.Order pOrder)
        {
            String lMessage = String.Format("Your order has been submitted successfully");
            try
            {
                ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                    CommandFactory.Instance.GetSubmitOrderCommand(pOrder)
                );
            }
            catch (Exception lException)
            {
                lMessage = lException.Message;
                throw;
            }
            finally
            {
                EmailProvider.SendMessage(pOrder.Customer.Email, lMessage);
            }

        }
    }
}
