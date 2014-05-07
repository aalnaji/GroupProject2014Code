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
using StockTicker.ViewModels;
using StockTicker.Views;
using StockTicker.Transformations;
using Common.Model;
using System.ServiceModel;
using Common;
using StockTicker.SubscriptionService;

namespace StockTicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Ideally, it would be better to use something like an event aggregator to 
        //publish events to this view model instead of making this public static
        //See EventAggregator under microsoft practices.
        public static TickerViewModel sViewModel;
        private global::Common.SubscriberServiceHost mHost;
        private const String cAddress = "net.msmq://localhost/private/TickerQueueTransacted";
        private const String cMexAddress = "net.tcp://localhost:9012/TickerQueueTransacted/mex";

        public MainWindow()
        {
            HostSubscriberService();

            this.Title = "Stock Ticker";
            sViewModel = new TickerViewModel();

            //SubscribeForEvents();
            InitializeComponent();
            TickerView lVIew = this.TickerView;
            lVIew.ViewModel = sViewModel;
            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mHost.Dispose();
        }

        private void HostSubscriberService()
        {
            mHost = new SubscriberServiceHost(typeof(SubscriberService), cAddress , cMexAddress, true, ".\\private$\\TickerQueueTransacted");
        }

        private void SubscribeForEvents()
        {
            SubscriptionServiceClient lClient = new SubscriptionServiceClient();
            lClient.Subscribe("Silver", cAddress);
            lClient.Subscribe("Gold", cAddress);
            lClient.Subscribe("Platinum", cAddress);
        }

        private void Subscribe_Click(object sender, RoutedEventArgs e)
        {
            SubscribeForEvents();
        }
    }
}
