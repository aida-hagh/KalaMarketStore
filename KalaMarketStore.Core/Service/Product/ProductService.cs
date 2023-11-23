using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;

namespace KalaMarketStore.Core.Service.Product
{

    public class ProductService : IProductService
    {

        private readonly AppDbContext context;
        public ProductService(AppDbContext context)
        {
            this.context = context;
        }

        public List<DataLayer.Entities.Products.Product> ShowAllProduct(int page)
        {
            int skip = (page - 1) * 4;
            return context.Products.Where(x => !x.IsDelete).Skip(skip).Take(4).ToList();
        }

        public List<DataLayer.Entities.Products.Product> ShowAllProduct()
        {

            return context.Products.Where(x => !x.IsDelete).ToList();
        }



        public int CountInPage()
        {
            int count = context.Products.Count();
            if (count % 4 != 0)
            {
                count++;
            }
            count = count / 4;
            return count;
        }

        public int AddProduct(DataLayer.Entities.Products.Product product)
        {
            try
            {
                context.Products.Add(product);
                context.SaveChanges();
                return product.ProductId;
            }
            catch (Exception)
            {

                return 0;
            }
        }


        public DataLayer.Entities.Products.Product FindProductById(int id)
        {
            return context.Products.Include(x=>x.Category).AsNoTracking().FirstOrDefault(x => x.ProductId == id);
        }

        public bool UpdateProduct(DataLayer.Entities.Products.Product product)
        {
            try
            {
                context.Products.Update(product);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(DataLayer.Entities.Products.Product product)
        {
            try
            {
                product.IsDelete = true;
                context.Products.Update(product);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int FindCategoryForProduct(int productId)
        {
            return context.Products.Where(x => x.ProductId == productId).Select(x => x.CategoryId).SingleOrDefault();

        }

        public List<DataLayer.Entities.Products.Property> ShowAllPropertyForCategory(int categoryId)
        {
            var propertys = (from
                           propertyCate in context.Category_Properties
                             join
                             prop in context.Properties
                             on
                             propertyCate.PropertyId equals prop.PropertyId
                             where (propertyCate.CategoryId == categoryId)
                             select new DataLayer.Entities.Products.Property
                             {
                                 PropertyId = prop.PropertyId,
                                 PropertyTitle = prop.PropertyTitle,
                             }).ToList();
            return propertys;
        }


        public bool DeletePropertyValuForProduct(int productid)
        {
            try
            {
                List<DataLayer.Entities.Products.PropertyValue> propertyValues = context.PropertyValues.Where(x => x.ProductId == productid).ToList();
                context.PropertyValues.RemoveRange(propertyValues);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }


        public bool AddOrUpdatePropertyForProduct(List<DataLayer.Entities.Products.PropertyValue> propertyValues, int productId)
        {
            try
            {
                if (DeletePropertyValuForProduct(productId))
                {
                    context.PropertyValues.AddRange(propertyValues);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public List<DataLayer.Entities.Products.PropertyValue> ShowPropertyValueForProduct(int productid)
        {
            return context.PropertyValues.Where(x => x.ProductId == productid).ToList();
        }




        #region Price

        public List<ShowPriceForProductViewModel> ShowAllPriceForProduct(int productId)
        {
            List<ShowPriceForProductViewModel> _Price = (from price in context.ProductPrices
                                                         join product in context.Products
                                                         on
                                                         price.ProductId equals product.ProductId
                                                         join color in context.ProductColors
                                                         on
                                                         price.ColorId equals color.ColorId
                                                         join guarantee in context.ProductGuarantees
                                                         on
                                                         price.GuaranteeId equals guarantee.GuaranteeId
                                                         where (price.ProductId == productId)
                                                         select new ShowPriceForProductViewModel
                                                         {
                                                             ColorName = color.ColorName,
                                                             Count = price.Count,
                                                             CreateDate = price.CreateDate,
                                                             EndDateDisCount = price.EndDateDisCount,
                                                             GuaranteeName = guarantee.GuaranteeName,
                                                             MainPrice = price.MainPrice,
                                                             MaxOrderCount = price.MaxOrderCount,
                                                             ProductId = product.ProductId,
                                                             PriceId = price.PriceId,
                                                             SpecialPrice = price.SpecialPrice,


                                                         }).ToList();

            return _Price;
        }


        public int AddPriceForeProduct(ProductPrice productPrice)
        {
            try
            {
                context.ProductPrices.Add(productPrice);
                context.SaveChanges();
                return productPrice.PriceId;

            }
            catch (Exception)
            {

                return 0;
            }
        }

        public ProductPrice FindPriceById(int priceId)
        {
            return context.ProductPrices.AsNoTracking().FirstOrDefault(x => x.PriceId == priceId);
        }
        public bool UpdatePrice(ProductPrice productPrice)
        {
            try
            {
                context.ProductPrices.Update(productPrice);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteProductPrice(ProductPrice productPrice)
        {
            try
            {
                context.ProductPrices.Remove(productPrice);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion



        public List<SpecialProductViewModel> ShowAllSpecialProduct()
        {
            List<SpecialProductViewModel> specials = (from product in context.Products
                                                      join price in context.ProductPrices
                                                      on product.ProductId equals price.ProductId
                                                      where (price.SpecialPrice < price.MainPrice && price.EndDateDisCount >= DateTime.Now.Date)
                                                      select new SpecialProductViewModel
                                                      {
                                                          ProductFaTitle = product.ProductFaTitle,
                                                          ProductId = product.ProductId,
                                                          ProductImage = product.ProductImage,
                                                          MainPrice = price.MainPrice,
                                                          SpecialPrice = price.SpecialPrice,
                                                          EndDateDisCount = price.EndDateDisCount,
                                                          ValuesList = (from property in context.Properties
                                                                        join propertyValue in context.PropertyValues
                                                                        on property.PropertyId equals propertyValue.PropertyId
                                                                        where (propertyValue.ProductId == product.ProductId)
                                                                        select new ValueViewModel
                                                                        {
                                                                            PropertyName = property.PropertyTitle,
                                                                            value = propertyValue.propertyValue
                                                                        }).Take(3).ToList()

                                                      }).ToList();
            return specials;


        }


        //اسلایدر محصولات آموزش ,استفاده نکردم
        public List<SliderForCategoryViewModel> ShowSliderProductForCategory(int categoryid)
        {
            List<SliderForCategoryViewModel> sliders = (from price in context.ProductPrices
                                                        join Product in context.Products
                                                        on price.ProductId equals Product.ProductId
                                                        join cate in context.Categories
                                                        on Product.CategoryId equals cate.CategoryId
                                                        select new SliderForCategoryViewModel
                                                        {
                                                            ProductFaTitle = Product.ProductFaTitle,
                                                            ProductId = Product.ProductId,
                                                            ProductImage = Product.ProductImage,
                                                            MainPrice = price.MainPrice,
                                                            SpecialPrice = price.EndDateDisCount >= DateTime.Now.Date && price.SpecialPrice < price.MainPrice ? price.SpecialPrice : price.MainPrice,
                                                            CategoryId = categoryid,
                                                            SubCategory = Product.CategoryId,

                                                        }).ToList();
            return sliders;

        }


        public List<ShowDeatailsProductViewModel> ShowDeatailsProducts(int productid)
        {
            List<ShowDeatailsProductViewModel> DeatailsPro = (from price in context.ProductPrices
                                                              join Product in context.Products
                                                              on price.ProductId equals Product.ProductId
                                                              join guarantee in context.ProductGuarantees
                                                              on price.GuaranteeId equals guarantee.GuaranteeId
                                                              join color in context.ProductColors
                                                              on price.ColorId equals color.ColorId
                                                              join brand in context.Brands
                                                              on Product.BrandId equals brand.BrandId
                                                              join category in context.Categories
                                                              on Product.CategoryId equals category.CategoryId
                                                              where (price.ProductId == productid)
                                                              select new ShowDeatailsProductViewModel
                                                              {
                                                                  ProductId = productid,
                                                                  ProductEnTitle = Product.ProductEnTitle,
                                                                  ProductFaTitle = Product.ProductFaTitle,
                                                                  ProductImage = Product.ProductImage,
                                                                  ProductSell = Product.ProductSell,
                                                                  ProductStars = Product.ProductStars,
                                                                  ProductTag = Product.ProductTag,
                                                                  MainPrice = price.MainPrice,
                                                                  BrandName = brand.BrandFaName,
                                                                  CategoryName = category.CategoryFaTitle,
                                                                  ColorCode = color.ColorCode,
                                                                  ColorName = color.ColorName,
                                                                  EndDateDisCount = price.EndDateDisCount,
                                                                  GuaranteeName = guarantee.GuaranteeName,
                                                                  SpecialPrice = price.SpecialPrice,
                                                                  PriceId = price.PriceId,

                                                                  //  SpecialPrice = price.SpecialPrice < price.MainPrice &&price.EndDateDisCount >= DateTime.Now.Date ? price.SpecialPrice : price.MainPrice,


                                                              }).ToList();
            return DeatailsPro;
        }


        public List<ValueViewModel> ShowValueForProduct(int productid)
        {
            List<ValueViewModel> values = (from prop in context.Properties
                                           join propVale in context.PropertyValues
                                           on prop.PropertyId equals propVale.PropertyId
                                           where (propVale.ProductId == productid)
                                           select new ValueViewModel
                                           {
                                               PropertyName = prop.PropertyTitle,
                                               value = propVale.propertyValue

                                           }).Take(4).ToList();
            return values;
        }


        public List<ProductGallery> ShowGalleryForProduct(int productid)
        {
            return context.ProductGalleries.Where(x => x.ProductId == productid).ToList();
        }


        public List<DataLayer.Entities.Products.Product> Search(string text, List<int> categoryid, List<int> brandid,
            int minprice = 0, int maxprice = 0, int sort = 1)
        {


            IQueryable<DataLayer.Entities.Products.Product> products =
                context.Products.Where(x => x.ProductFaTitle.Contains(text) || x.ProductEnTitle.Contains(text));

            //IQueryable<DataLayer.Entities.Products.Product> products = context.Products
            //    .Include(x => x.Category)
            //    .OrderByDescending(x => x.ProductCreate);

            //if (id != 0 )
            //{
            //    products = context.Products.Where(x => x.SubCategoryId == id);
            //}

            //if (!string.IsNullOrWhiteSpace(text))
            //{
            //    products = context.Products.Where(x => x.ProductFaTitle.Contains(text) 
            //    || x.ProductEnTitle.Contains(text));
            //}



            switch (sort)
            {
                case 1:
                    products = products.OrderByDescending(x => x.ProductSell);
                    break;
                case 2:
                    products = products.OrderBy(x => x.ProductPrices.First().MainPrice);
                    break;
                case 3:
                    products = products.OrderByDescending(x => x.ProductCreate);
                    break;
                case 4:
                    products = products.OrderByDescending(x => x.ProductStars);
                    break;
                case 5:
                    products = products.OrderByDescending(x => x.ProductPrices.First().MainPrice);
                    break;
            }


            if (categoryid.Count() > 0)
            {
                products = products.Where(x => categoryid.Contains(x.SubCategoryId.Value) || categoryid.Contains(x.CategoryId) || categoryid.Contains(x.SecondSubCategoryId.Value));
            }

            if (brandid.Count() > 0)
            {
                products = products.Where(x => brandid.Contains(x.BrandId));
            }

            var query = (from product in products
                         join pr in context.ProductPrices
                         on product.ProductId equals pr.ProductId
                         select new
                         {
                             productStar = product.ProductStars,
                             imgpro = product.ProductImage,
                             titleFa = product.ProductFaTitle,
                             mainPrice = pr.MainPrice,
                             specialPrice = pr.SpecialPrice,
                             productid = product.ProductId,
                         }).ToList();

            List<DataLayer.Entities.Products.Product> listProduct = new List<DataLayer.Entities.Products.Product>();

            foreach (var item in products)
            {
                listProduct.Add(new DataLayer.Entities.Products.Product
                {
                    ProductStars = item.ProductStars,
                    ProductImage = item.ProductImage,
                    ProductFaTitle = item.ProductFaTitle,

                    MainPrice = query.Where(x => x.productid == item.ProductId).OrderBy(x => x.mainPrice)
                    .Select(x => x.mainPrice).FirstOrDefault(),

                    SpecialPrice = query.Where(x => x.productid == item.ProductId)
                    .OrderBy(x => x.specialPrice)
                    .Select(x => x.specialPrice)
                    .FirstOrDefault(),
                });
            }

            if (minprice > 0)
            {
                listProduct = listProduct.Where(x => x.MainPrice >= minprice && x.MainPrice > 0).ToList();
            }

            if (maxprice > 0)
            {
                listProduct = listProduct.Where(x => x.MainPrice <= maxprice && x.MainPrice > 0).ToList();
            }

            return listProduct;
        }






        //public bool ExistDuplicateproduct(int productid,string proname)
        //{
        //    return context.Products.Any(x => x.ProductFaTitle == proname && x.ProductId != productid);
        //}

        //public List<DataLayer.Entities.Products.Product> GetProductForCategory(int? id)
        //{
        //    var model = context.Products.Include(x => x.Category).Where(x=>x.Category.SubCategiry==id).ToList();
        //    if (model !=null)
        //    {
        //        return model;
        //    }

        //}
        //public List<ShowProductForCategoryInMenu> GetProductForCategory2(int? id)
        //{
        //    var proCate = (from p in context.Products
        //                   join pr in context.ProductPrices
        //            on p.ProductId equals pr.ProductId
        //                   join cate in context.Categories
        //                   on p.CategoryId equals cate.CategoryId
        //                   where (cate.SubCategiry == id)
        //                   select new ShowProductForCategoryInMenu
        //                   {
        //                       CategoryTitle = cate.CategoryFaTitle,
        //                       SubCategory = id,
        //                       MainPrice = pr.MainPrice,
        //                       SpecialPrice = p.SpecialPrice,
        //                       ProductFaTitle = p.ProductFaTitle,
        //                       ProductImage = p.ProductImage,
        //                       ProductStars = p.ProductStars,
        //                       ProductId = p.ProductId

        //                   }).ToList();
        //    return proCate;

        //}






    }


}


