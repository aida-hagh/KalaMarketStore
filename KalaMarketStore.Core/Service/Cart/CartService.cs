using KalaMarketStore.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalaMarketStore.DataLayer.Entities;
using KalaMarketStore.Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using KalaMarketStore.Core.ExtentionMethod;

namespace KalaMarketStore.Core.Service.Cart
{
    public class CartService : ICartService
    {
        private readonly AppDbContext Context;
        public CartService(AppDbContext Context)
        {
            this.Context = Context;
        }


        public int AddCart(int userid, int productPriceid)
        {
            DataLayer.Entities.Cart cart = Context.Carts.SingleOrDefault(c => !c.IsPay && c.UserId == userid);

            var product = Context.ProductPrices.FirstOrDefault(x => x.PriceId == productPriceid);
            if (cart == null)
            {
                cart = new DataLayer.Entities.Cart
                {
                    CreateData = DateTime.Now,
                    IsPay = false,
                    PaymentCode = "",
                    UserId = userid,
                    TotalPrice = product.SpecialPrice < product.MainPrice
                    && Convert.ToDateTime(product.EndDateDisCount).Date < DateTime.Now ? product.SpecialPrice.Value : product.MainPrice,

                    CartDeatails = new List<CartDeatail>

                    {
                       new CartDeatail
                       {
                      Count=1,
                      CreateDate=DateTime.Now,
                      ProductPriceId=product.PriceId,
                      Price=product.SpecialPrice < product.MainPrice
                      && Convert.ToDateTime(product.EndDateDisCount).Date < DateTime.Now ? product.SpecialPrice.Value : product.MainPrice,

                       }
                    }
                };

                Context.Carts.Add(cart);
                Context.SaveChanges();
                return 2;
            }
            else
            {
                CartDeatail cartDeatail = Context.CartDeatails.FirstOrDefault(c => c.CartId == cart.CartId&&c.ProductPriceId == productPriceid);

                if (cartDeatail != null && cartDeatail.Count < product.MaxOrderCount)
                {
                    //با کد سطر زیر اول کم میکنه قبلی هارو بعد با سطر اخر جدیدارو حساب میکنه
                    DecreasePrice(cartDeatail.CartId, cartDeatail.CartDeatailId);
                    cartDeatail.Count += 1;
                    Context.CartDeatails.Update(cartDeatail);
                    SumPrice(cartDeatail.CartId, cartDeatail.CartDeatailId);
                    return 2;
                }
                else if (cartDeatail == null)
                {
                    cartDeatail = new CartDeatail

                    {
                        CartId = cart.CartId,
                        Count = 1,
                        CreateDate = DateTime.Now,
                        ProductPriceId = product.PriceId,
                        Price = product.SpecialPrice < product.MainPrice
                        && Convert.ToDateTime(product.EndDateDisCount).Date >= DateTime.Now ? product.SpecialPrice.Value : product.MainPrice,

                    };
                    Context.Add(cartDeatail);
                    Context.SaveChanges();
                    SumPrice(cartDeatail.CartId, cartDeatail.CartDeatailId);
                    return 2;
                }
            }

            return 3;

        }


        public void SumPrice(int cartid, int deatailid)
        {

            var cart = Context.Carts.Find(cartid);

            var deatail = Context.CartDeatails.Where(x => x.CartId == cartid && x.CartDeatailId == deatailid).FirstOrDefault();
            cart.TotalPrice += deatail.Count * deatail.Price;
            Context.Update(cart);
            Context.SaveChanges();

        }


        public void DecreasePrice(int cartid, int deatailid)
        {

            var cart = Context.Carts.Find(cartid);

            var deatail = Context.CartDeatails.Where(x => x.CartId == cartid && x.CartDeatailId == deatailid).FirstOrDefault();
            cart.TotalPrice -= deatail.Count * deatail.Price;
            Context.Update(cart);
            Context.SaveChanges();

        }

      
        public List<CartViewModel> DeatailCart(int userid)
        {
            List<CartViewModel> carts = (from c in Context.Carts
                                         join cd in Context.CartDeatails
                                         on c.CartId equals cd.CartId

                                         join pr in Context.ProductPrices
                                         on cd.ProductPriceId equals pr.PriceId

                                         join p in Context.Products
                                         on pr.ProductId equals p.ProductId

                                         join color in Context.ProductColors
                                         on pr.ColorId equals color.ColorId


                                         join g in Context.ProductGuarantees
                                         on pr.GuaranteeId equals g.GuaranteeId
                                         where (c.UserId == userid && !c.IsPay)
                                         select new CartViewModel
                                         {
                                             ProductId = p.ProductId,
                                             ProductFaName = p.ProductFaTitle,
                                             ColorName = color.ColorName,
                                             GuaranteeName = g.GuaranteeName,
                                             OrderCount = cd.Count,
                                             ProductPrice = cd.Price,
                                             PriceId = pr.PriceId,
                                             ProductImg = p.ProductImage,
                                             TotalPrice = c.TotalPrice,
                                             MaxOrderCount = pr.MaxOrderCount,
                                             ProductCount = pr.Count,
                                             CartId = cd.CartId,
                                             ColorCode=color.ColorCode,

                                         }).ToList();
            return carts;
        }


        public int ChangeBasket(int userid, int productPriceid, int count)
        {
            DataLayer.Entities.Cart cart = Context.Carts.SingleOrDefault(c => !c.IsPay && c.UserId == userid);

            var product = Context.ProductPrices.FirstOrDefault(x => x.PriceId == productPriceid);
            if (cart == null)
            {
                cart = new DataLayer.Entities.Cart
                {
                    CreateData = DateTime.Now,
                    IsPay = false,
                    PaymentCode = "",
                    UserId = userid,
                    TotalPrice = product.SpecialPrice < product.MainPrice
                    && Convert.ToDateTime(product.EndDateDisCount).Date < DateTime.Now ? product.SpecialPrice.Value : product.MainPrice,

                    CartDeatails = new List<CartDeatail>

                    {
                       new CartDeatail
                       {
                      Count=count,
                      CreateDate=DateTime.Now,
                      ProductPriceId=productPriceid,
                      Price=product.SpecialPrice < product.MainPrice
                      && Convert.ToDateTime(product.EndDateDisCount).Date < DateTime.Now ? product.SpecialPrice.Value : product.MainPrice,

                       }
                    }
                };

                Context.Carts.Add(cart);
                Context.SaveChanges();
                return 2;
            }
            else
            {
                CartDeatail cartDeatail = Context.CartDeatails.FirstOrDefault(c => c.CartId == cart.CartId);

                if (cartDeatail != null && cartDeatail.Count <= product.MaxOrderCount)
                {
                    //با کد سطر زیر اول کم میکنه قبلی هارو بعد با سطر اخر جدیدارو حساب میکنه
                    DecreasePrice(cartDeatail.CartId, cartDeatail.CartDeatailId);
                    cartDeatail.Count = count;
                    Context.CartDeatails.Update(cartDeatail);
                    SumPrice(cartDeatail.CartId, cartDeatail.CartDeatailId);
                    return 2;
                }
                else if (cartDeatail == null)
                {
                    cartDeatail = new CartDeatail

                    {
                        CartId = cart.CartId,
                        Count = count,
                        CreateDate = DateTime.Now,
                        ProductPriceId = product.PriceId,
                        Price = product.SpecialPrice < product.MainPrice
                        && Convert.ToDateTime(product.EndDateDisCount).Date < DateTime.Now ? product.SpecialPrice.Value : product.MainPrice,

                    };
                    Context.Add(cartDeatail);
                    Context.SaveChanges();
                    SumPrice(cartDeatail.CartId, cartDeatail.CartDeatailId);
                    return 2;
                }
            }

            return 3;

        }


        public void RemoveProductFromBasket(int productPriceId, int cartId)
        {
            var basket = Context.CartDeatails.Where(x => x.ProductPriceId == productPriceId && x.CartId == cartId).FirstOrDefault();
            DecreasePrice(basket.CartId, basket.CartDeatailId);
            Context.Remove(basket);
            Context.SaveChanges();

        }

        public void UpdateCart(DataLayer.Entities.Cart cart)
        {
            Context.Update(cart);
            Context.SaveChanges();
        }

        public List<ShowBasketViewModel> ShowAllProductForBasket(int userId)
        {
           return (from c in Context.Carts
                     where (c.UserId == userId && !c.IsPay)
                     join cd in Context.CartDeatails on c.CartId equals cd.CartId

                     join pr in Context.ProductPrices on cd.ProductPriceId equals pr.PriceId
                     join p in Context.Products on pr.ProductId equals p.ProductId
                     select new ShowBasketViewModel
                     {
                         MainPrice=(pr.MainPrice>pr.SpecialPrice&&pr.EndDateDisCount>=DateTime.Now.Date)?pr.SpecialPrice.Value:pr.MainPrice,
                         ProductFaTitle=p.ProductFaTitle,
                         ProductId=p.ProductId,
                         ProductImage=p.ProductImage,
                         TotalBasketPrice=c.TotalPrice,
                     }).ToList();   
                    
        }


        public DataLayer.Entities.Cart FindCartByUserId(int userId)
        {
            return Context.Carts.Where(x => !x.IsPay && x.UserId == userId).FirstOrDefault();
        }


        public DataLayer.Entities.Cart FindCartById(int cartid)
        {
            return Context.Carts.Find(cartid);
        }


        public List<highchartViewModel> highchart()
        {

            var chart = Context.Carts.Where(x => x.IsPay).GroupBy(x => x.CreateData.Date).OrderByDescending(x=>x.Key).Take(7).Select(x => new highchartViewModel
            {
                name = x.Key.MiladiToShamsi(),
                y = x.Count(),
            }).ToList();

            return chart.OrderBy(x=>x.name).ToList();
        }


        public List<CartViewModel> DeatailOrder(int cartid)
        {
            List<CartViewModel> carts = (from c in Context.Carts
                                         join cd in Context.CartDeatails
                                         on c.CartId equals cd.CartId

                                         join pr in Context.ProductPrices
                                         on cd.ProductPriceId equals pr.PriceId

                                         join p in Context.Products
                                         on pr.ProductId equals p.ProductId

                                         join color in Context.ProductColors
                                         on pr.ColorId equals color.ColorId


                                         join g in Context.ProductGuarantees
                                         on pr.GuaranteeId equals g.GuaranteeId
                                         where(cd.CartId==cartid)
                                         select new CartViewModel
                                         {
                                             ProductId = p.ProductId,
                                             ProductFaName = p.ProductFaTitle,
                                             ColorName = color.ColorName,
                                             GuaranteeName = g.GuaranteeName,
                                             OrderCount = cd.Count,
                                             ProductPrice = cd.Price,
                                             PriceId = pr.PriceId,
                                             ProductImg = p.ProductImage,
                                             TotalPrice = c.TotalPrice,
                                             MaxOrderCount = pr.MaxOrderCount,
                                             ProductCount = pr.Count,
                                             CartId = cd.CartId,
                                             ColorCode = color.ColorCode,

                                         }).ToList();
            return carts;
        }


    }
}
 