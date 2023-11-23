using KalaMarketStore.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Brand
{
    public interface IBrandService
    {
        List<DataLayer.Entities.Products.Brand> ShowAllBrand(int page);
        IEnumerable<DataLayer.Entities.Products.Brand> ShowAllBrand();

        int AddBrand(DataLayer.Entities.Products.Brand brand);

        bool UpdateBrand(DataLayer.Entities.Products.Brand brand);

        bool DeleteBrand(DataLayer.Entities.Products.Brand brand);

        bool ExistBrand(string FaName,string EnName, int BrandId);

        DataLayer.Entities.Products.Brand FindBrandById(int brandId);

        int CountInPage();
    }
}
