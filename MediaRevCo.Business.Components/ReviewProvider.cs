using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MediaRevCo.Business.Entities;
using MediaRevCo.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace MediaRevCo.Business.Components
{

    //NOTE: THIS CODE IS NOT THREAD-SAFE!!!!
    //Ideally Reviews would be saved to the database, but to keep things simple
    //for this assignment, we are using a static collection
    
    public class ReviewProvider : IReviewProvider
    {
        private static ObservableCollection<Review> sReviewCollection = null;

        static ReviewProvider()
        {
            sReviewCollection = new ObservableCollection<Review>();
            sReviewCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(sReviewCollection_CollectionChanged);
        }

        static void sReviewCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                IReviewSubscriptionProvider lSubscriptionProvider = ServiceLocator.Current.GetInstance<IReviewSubscriptionProvider>();
                foreach (Review lReview in e.NewItems)
                {
                    lSubscriptionProvider.PublishReview(lReview);
                }
            }
        }

        public void AddReview(Review pReview)
        {
            sReviewCollection.Add(pReview);
        }


        public ObservableCollection<Review> GetReviews()
        {
            return sReviewCollection;
        }
    }
}
