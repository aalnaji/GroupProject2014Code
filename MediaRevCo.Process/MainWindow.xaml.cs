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
