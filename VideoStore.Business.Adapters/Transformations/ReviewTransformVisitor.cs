using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using VideoStore.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;
using VideoStore.Business.Entities;

namespace VideoStore.Business.Adapters.Transformations
{
    public static class Extensions
    {
        public static void Accept(this MediaRevCo.Business.Entities.Review pReview, IVisitor pVisitor)
        {
            pVisitor.Visit(pReview);
        }
    }

    public class ReviewTransformVisitor : IVisitor
    {
        public VideoStore.Business.Entities.Review Result
        {
            get;
            set;
        }

        public ICatalogueProvider CatProvider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ICatalogueProvider>();
            }
        }

        public IUserProvider UserProvider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUserProvider>();
            }
        }

        



        public void Visit(object pVisitable)
        {
            if(pVisitable is MediaRevCo.Business.Entities.Review)
            {
                
                MediaRevCo.Business.Entities.Review lFrom = pVisitable as MediaRevCo.Business.Entities.Review;
                Media lMedia = CatProvider.GetMediaByUPC(lFrom.UPC);
                VideoStore.Business.Entities.Review lRev = new VideoStore.Business.Entities.Review()
                {
                    Comments = lFrom.Comments,
                    Title = lFrom.ReviewTitle,
                    Media = lMedia,
                    //here we should fetch "system" usr
                    Users = GetSystemUser()
                };
                Result = lRev;
            }
        }

        private User GetSystemUser()
        {
            return UserProvider.ReadUserById(1);
        }
    }
}
