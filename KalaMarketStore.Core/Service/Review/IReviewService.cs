using KalaMarketStore.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Review
{
    public interface IReviewService
    {

     
        DataLayer.Entities.Products.Review FindReviewById(int productid);
        bool AddOrUpdateReview(DataLayer.Entities.Products.Review review);

        bool DeleteReview(int productid);
        List<ValueViewModel> ShowAllPropertyValuForProduct(int productid);

    }
}
