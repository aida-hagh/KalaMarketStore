using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace KalaMarketStore.Core.Service.Product
{
    public interface IProductService
    {

        List<DataLayer.Entities.Products.Product> ShowAllProduct(int page);
        List<DataLayer.Entities.Products.Product> ShowAllProduct();

        int AddProduct(DataLayer.Entities.Products.Product product);

        int CountInPage();

        DataLayer.Entities.Products.Product FindProductById(int id);

        bool UpdateProduct(DataLayer.Entities.Products.Product product);
        bool DeleteProduct(DataLayer.Entities.Products.Product product);
        public int FindCategoryForProduct(int productId);
        List<DataLayer.Entities.Products.Property> ShowAllPropertyForCategory(int categoryId);
        bool DeletePropertyValuForProduct(int productid);
        bool AddOrUpdatePropertyForProduct(List<DataLayer.Entities.Products.PropertyValue> propertyValues, int productId);

        List<DataLayer.Entities.Products.PropertyValue> ShowPropertyValueForProduct(int productid);




        List<ShowPriceForProductViewModel> ShowAllPriceForProduct(int productId);

        int AddPriceForeProduct(ProductPrice productPrice);

        ProductPrice FindPriceById(int priceId);

        bool UpdatePrice(ProductPrice productPrice);

        bool DeleteProductPrice(ProductPrice productPrice);


        List<SpecialProductViewModel> ShowAllSpecialProduct();

        List<SliderForCategoryViewModel> ShowSliderProductForCategory(int categoryid);

        List<ShowDeatailsProductViewModel> ShowDeatailsProducts(int productid);

        List<ValueViewModel> ShowValueForProduct(int productid);

        List<ProductGallery> ShowGalleryForProduct(int productid);

        List<DataLayer.Entities.Products.Product> Search (string text, List<int> categoryid,
           List<int> brandid, int minprice = 0, int maxprice = 0, int sort = 1);

       // List<ShowProductForCategoryInMenu> GetProductForCategory2(int? id);

    }
}
