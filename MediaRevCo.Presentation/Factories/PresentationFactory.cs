using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaRevCo.Presentation.ViewModels;
using System.Collections.ObjectModel;
using MediaRevCo.Business.Entities;

namespace MediaRevCo.Presentation.Factories
{
    public class PresentationFactory
    {
        private static PresentationFactory sFactory;

        public static PresentationFactory Instance
        {
            get
            {
                if (sFactory == null)
                {
                    sFactory = new PresentationFactory();
                }
                return sFactory;
            }
        }

        public ReviewListViewModel GetReviewListViewModel()
        {
            return new ReviewListViewModel();
        }

        public ReviewAdderViewModel GetReviewAdderViewModel()
        {
            return new ReviewAdderViewModel();
        }
    }
}
