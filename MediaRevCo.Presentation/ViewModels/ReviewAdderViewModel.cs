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
    public class ReviewAdderViewModel
    {
        private IReviewProvider Provider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IReviewProvider>();
            }
        }

        public ReviewAdderViewModel()
        {
        }

        public void AddReview(Review pReview)
        {
            Provider.AddReview(pReview);
        }
    }
}
