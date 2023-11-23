using KalaMarketStore.Core.Service.Cart;
using KalaMarketStore.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.DisCount
{
    public class DisCountService : IDisCountService
    {
        private readonly AppDbContext context;
        private readonly ICartService cartService;
        public DisCountService(AppDbContext context, ICartService cartService)
        {
            this.context = context;
            this.cartService = cartService; 
        }


        public int CheckDisCount(int cartid, string discountCode)
        {

            var DisCount = context.DisCounts.FirstOrDefault(x => x.DisCountCode == discountCode);

            if (DisCount == null)
                return 1;        //کدتخفیف وارد شده نامعتبر میباشد.


            //if (DisCount.StartDate != null && DisCount.StartDate > DateTime.Now.Date)
            //    return 2;        // تاریخ کد تخفیف به پایان رسیده است.

            if (DisCount.EndDate != null && DisCount.EndDate < DateTime.Now.Date)
                return 2;        // تاریخ کد تخفیف به پایان رسیده است.

            if (DisCount.UseableCount != null && DisCount.UseableCount <= 0)
                return 3;        // تعداد کد تخفیف به پایان رسیده است.

            var cart = context.Carts.Find(cartid);
            if (context.UserDisCounts.Any(x => x.UserId == cart.UserId && x.DisCountId == DisCount.DisCountId))
                return 4;        // شما قبلا از این کد تخفیف استفاده کرده اید.

            int persent = (cart.TotalPrice * DisCount.DisCountPersent) / 100;

            cart.TotalPrice = cart.TotalPrice - persent;
            cartService.UpdateCart(cart);
            if (DisCount.UseableCount != null)
                DisCount.UseableCount -= 1;
            context.Update(DisCount);
            context.UserDisCounts.Add(new DataLayer.Entities.Discount.UserDisCount
            {
                DisCountId=DisCount.DisCountId, 
                UserId=cart.UserId, 
            });

            context.SaveChanges();  
            return 5;              // کد تخفیف با موفقیت اعمال شد . 

        }


    }
}
