using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoStore.Business.Entities;
using VideoStore.WebClient.CatalogueService;

namespace VideoStore.WebClient.ViewModels
{
    public class ListReviewsViewModel
    {
        public Media Media { get; set; }

        public ListReviewsViewModel(int pMediaId)
        {
            Media = new CatalogueServiceClient().GetMediaById(pMediaId);
        }
    }
}