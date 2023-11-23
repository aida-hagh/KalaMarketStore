using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Review
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext context;
        public ReviewService(AppDbContext context)
        {
            this.context = context;
        }



        public DataLayer.Entities.Products.Review FindReviewById(int productid)
        {
            return context.Reviews.Where(x => x.ProductId == productid).FirstOrDefault();
        }

        public bool AddOrUpdateReview(DataLayer.Entities.Products.Review review)
        {
            try
            {
                context.Reviews.Add(review);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteReview(int productid)
        {
            try
            {
                var review = context.Reviews.Where(x => x.ProductId == productid).FirstOrDefault();
                if (review != null)
                {
                    context.Reviews.Remove(review);
                    context.SaveChanges();
                    return true;
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<ValueViewModel>ShowAllPropertyValuForProduct(int productid)
        {
            List<ValueViewModel> values = (from prop in context.Properties
                                           join propVale in context.PropertyValues
                                           on prop.PropertyId equals propVale.PropertyId
                                           where(propVale.ProductId == productid)
                                           select new ValueViewModel
                                           {
                                               PropertyName = prop.PropertyTitle,
                                               value = propVale.propertyValue

                                           }).ToList();
            return values;
        }
    }
}
