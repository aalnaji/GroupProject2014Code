using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaRevCo.Business.Entities;
using System.Collections.ObjectModel;

namespace MediaRevCo.Business.Components.Interfaces
{
    public interface IReviewProvider
    {
        void AddReview(Review pReview);
        ObservableCollection<Review> GetReviews();
    }
}
