using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaRevCo.Presentation.Factories;
using MediaRevCo.Presentation.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.ServiceLocatorAdapter;
using Microsoft.Practices.ServiceLocation;
using System.Configuration;
using System.ServiceModel;
using MediaRevCo.Services;
using MediaRevCo.Services.Interfaces;

namespace MediaRevCo.Process
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResolveDependencies();
           
            ReviewListViewModel lModel = PresentationFactory.Instance.GetReviewListViewModel();
            this.ReviewList.DataContext = lModel;
            this.ReviewAdder.DataContext = PresentationFactory.Instance.GetReviewAdderViewModel();
            HostService();
            
        }

        private void HostService()
        {
            ServiceHost lHost = new ServiceHost(typeof(ReviewSubscriptionService));
            //Code start - new
            // Add MSMQ service end point to the subscription service
            NetMsmqBinding binding = new NetMsmqBinding(NetMsmqSecurityMode.None)
            {
                ExactlyOnce = false
            };

            lHost.AddServiceEndpoint(typeof(IReviewSubscriptionService), binding,
               "net.msmq://localhost/private/SubscribeQ");

            //Create private queue if not exists
            string queueName = @".\private$\SubscribeQ";
            if (!System.Messaging.MessageQueue.Exists(queueName))
                System.Messaging.MessageQueue.Create(queueName, false);
            //Code end - new

            lHost.Open();
        }

        private static void ResolveDependencies()
        {

            UnityContainer lContainer = new UnityContainer();
            UnityConfigurationSection lSection
                    = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
            lSection.Containers["containerOne"].Configure(lContainer);
            UnityServiceLocator locator = new UnityServiceLocator(lContainer);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
