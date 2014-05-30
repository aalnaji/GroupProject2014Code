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
            }
            finally
            {
                EmailProvider.SendMessage(pOrder.Customer.Email, lMessage);
            }
        }
        // new code start: 
        public void ReverseOrder(int pOrderId) 
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                int orderQuantity = 0;
                int mediaQuantity = 0;

                List<OrderItem> orderItemModel = (from order in lContainer.Orders
                                                  from orderItem in order.OrderItems
                                                  where order.Id.Equals(pOrderId)
                                                  select orderItem).ToList();
                foreach (OrderItem orderItem in orderItemModel)
                {
                    orderQuantity = orderItem.Quantity;
                    // p.s. ahmed
                    Stock stockModel = lContainer.Stocks.Where(x => x.Media.Id == orderItem.Media.Id).FirstOrDefault();
                    mediaQuantity = stockModel.Quantity;
                    stockModel.Quantity = mediaQuantity + orderQuantity;
                }
                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        // new code start:
        public void PersistOrder(string pMessage, int pOrderId)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                Order orderModel = lContainer.Orders.Where(x => x.Id == pOrderId).FirstOrDefault();
                if (pMessage.Equals("TransferSuccessful"))
                {
                    orderModel.Status = 1;
                }
                else
                {
                    orderModel.Status = 2;
                }
                lContainer.SaveChanges();
                lScope.Complete();
            }
        }
    }
}
