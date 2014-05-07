using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MediaRevCo.Business.Entities;
using MediaRevCo.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace MediaRevCo.Presentation.ViewModels
{
    public class ReviewListViewModel
    {

        private IReviewProvider Provider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IReviewProvider>();
            }
        }

        public ObservableCollection<Review> List
        {
            get { return Provider.GetReviews(); }
        }

        public ReviewListViewModel()
        {
        }
    }
}
