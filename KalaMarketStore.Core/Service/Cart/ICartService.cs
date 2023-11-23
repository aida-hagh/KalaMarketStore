using KalaMarketStore.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Cart
{
    public interface ICartService
    {
        int AddCart(int userid, int productid);

        List<CartViewModel> DeatailCart(int userid);

        int ChangeBasket(int userid, int productid, int count);

        void RemoveProductFromBasket(int productPriceId, int cartId);

        void UpdateCart(DataLayer.Entities.Cart cart);
        List<ShowBasketViewModel> ShowAllProductForBasket(int userId);

        DataLayer.Entities.Cart FindCartByUserId(int userId);
        DataLayer.Entities.Cart FindCartById(int cartId);

        List<highchartViewModel> highchart();

        List<CartViewModel> DeatailOrder(int cartid);
    }
}
