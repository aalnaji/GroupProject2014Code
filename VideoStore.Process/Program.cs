using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using VideoStore.Services;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.ComponentModel.Composition.Hosting;
using VideoStore.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.ServiceLocatorAdapter;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using VideoStore.Business.Entities;
using System.Transactions;
using System.ServiceModel.Description;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Adapters;
using MessageBus.Interfaces;
using VideoStore.Common;

namespace VideoStore.Process
{
    public class Program
    {
        static void Main(string[] args)
        {
            ResolveDependencies();
            StartAdapters();
            InsertDummyEntities();
            HostServices();
        }

        private static void StartAdapters()
        {
            (new BankAdapter()).Start();
            (new VideoStoreAdapter()).Start();
            (new MediaReviewsCompanyAdapter()).Start();
        }

        private static void InsertDummyEntities()
        {
            CreateOperator();
            CreateUser();
            InsertCatalogueEntities();           
        }

        private static void InsertCatalogueEntities()
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                if (lContainer.Medias.Count() == 0)
                {
                    Media lGreatExpectations = new Media()
                    {
                        Director = "Rene Clair",
                        Genre = "Fiction",
                        Price = 20.0M,
                        Title = "And Then there were None",
                        UPC="A"
                    };

                    //lContainer.Media.AddObject(lGreatExpectations);
                    //ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                    //    CommandFactory.Instance.GetEntityInsertCommand<Media>(lGreatExpectations)
                    //);

                    Stock lGreatExpectationsStock = new Stock()
                    {
                        Media = lGreatExpectations,
                        Holding = 5,
                        Warehouse = "Neutral Bay",
                        Quantity = 1
                    };

                    //lContainer.Stocks.AddObject(lGreatExpectationsStock);
                    //ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                    //    CommandFactory.Instance.GetEntityInsertCommand<Stock>(lGreatExpectationsStock)
                    //);
                    ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                        CommandFactory.Instance.GetEntityInsertCommand<Media>(lGreatExpectations)
                    );


                    Media lSoloist = new Media()
                    {
                        Director = "The Soloist",
                        Genre = "Fiction",
                        Price = 15.0M,
                        Title = "The Soloist",
                        UPC="B"
                    };

                    //lContainer.Media.AddObject(lSoloist);
                    //ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                    //    CommandFactory.Instance.GetEntityInsertCommand<Media>(lSoloist)
                    //);


                    Stock lSoloistStock = new Stock()
                    {
                        Media = lSoloist,
                        Holding = 7,
                        Warehouse = "Neutral Bay",
                        Quantity = 1
                    };

                    //lContainer.Stocks.AddObject(lSoloistStock);
                    //ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                    //    CommandFactory.Instance.GetEntityInsertCommand<Stock>(lSoloistStock)
                    //);
                    ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                        CommandFactory.Instance.GetEntityInsertCommand<Media>(lSoloist)
                    );



                    for (int i = 0; i < 30; i++)
                    {
                        Media lItem = new Media()
                        {
                            Director = String.Format("Director {0}", i.ToString()),
                            Genre = String.Format("Genre {0}", i),
                            Price = i,
                            Title = String.Format("Title {0}", i),
                            UPC = i.ToString()
                        };

                        //lContainer.Media.AddObject(lItem);
                        //ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                        //    CommandFactory.Instance.GetEntityInsertCommand<Media>(lItem)
                        //);

                        Stock lStock = new Stock()
                        {
                            Media = lItem,
                            Holding = 7,
                            Warehouse = String.Format("Warehouse {0}", i),
                            Quantity = 1
                        };

                        //lContainer.Stocks.AddObject(lStock);
                        //ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                        //    CommandFactory.Instance.GetEntityInsertCommand<Stock>(lStock)
                        //);
                        ServiceLocator.Current.GetInstance<IPublisherService>().Publish(
                            CommandFactory.Instance.GetEntityInsertCommand<Media>(lItem)
                        );

                    }


                    //lContainer.SaveChanges();
                    lScope.Complete();
                }

                

            }

        }

        private static void CreateOperator()
        {
            Role lOperatorRole = new Role() { Name = "Operator" };
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                if (lContainer.Roles.Count() > 0)
                {
                    return;
                }
            }
            User lOperator = new User()
            {
                Name = "Operator",
                LoginCredential = new LoginCredential() { UserName = "Operator", Password = "Operator" },
                Email = "Operator@Operator.com",
                Address = "1 Central Park"
            };

            lOperator.Roles.Add(lOperatorRole);

            ServiceLocator.Current.GetInstance<IUserProvider>().CreateUser(lOperator);
        }
        private static void CreateUser()
        {
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                if (lContainer.Users.Where((pUser) => pUser.Name == "Customer").Count() > 0)
                    return;
            }
            User lCustomer = new User()
            {
                Name = "Customer",
                LoginCredential = new LoginCredential() { UserName = "Customer", Password = "Customer" },
                Email = "test@test.com",
                Address = "2 Central Park",
                BankAccountNumber = 456,
            };

            ServiceLocator.Current.GetInstance<IUserProvider>().CreateUser(lCustomer);
        }

        private static void ResolveDependencies()
        {

            UnityContainer lContainer = new UnityContainer();
            UnityConfigurationSection lSection
                    = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            lSection.Containers["containerOne"].Configure(lContainer);
            UnityServiceLocator locator = new UnityServiceLocator(lContainer);
            ServiceLocator.SetLocatorProvider(() => locator);
        }


        private static void HostServices()
        {
            List<ServiceHost> lHosts = new List<ServiceHost>();
            try
            {

                Configuration lAppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ServiceModelSectionGroup lServiceModel = ServiceModelSectionGroup.GetSectionGroup(lAppConfig);

                System.ServiceModel.Configuration.ServicesSection lServices = lServiceModel.Services;
                foreach (ServiceElement lServiceElement in lServices.Services)
                {
                    ServiceHost lHost = new ServiceHost(Type.GetType(GetAssemblyQualifiedServiceName(lServiceElement.Name)));
                    lHost.Open();
                    lHosts.Add(lHost);
                }
                Console.WriteLine("Service Started, press Q key to quit");
                while (Console.ReadKey().Key != ConsoleKey.Q) ;
            }
            finally
            {
                foreach (ServiceHost lHost in lHosts)
                {
                    lHost.Close();
                }
            }
        }

        private static String GetAssemblyQualifiedServiceName(String pServiceName)
        {
            return String.Format("{0}, {1}", pServiceName, System.Configuration.ConfigurationManager.AppSettings["ServiceAssemblyName"].ToString());
        }
    }
}
