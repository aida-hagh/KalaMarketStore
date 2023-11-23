using KalaMarketStore.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Color
{
    public interface IColorService
    {
        #region Color

        Task<IEnumerable<ProductColor>> ShowAllColor(int page);
        Task<List<ProductColor>> ShowAllColor();
        Task<int> AddColor(ProductColor productColor);
        Task<bool> UpdateColor(ProductColor productColor);
        Task<ProductColor> FindColorById(int colorId);
        Task<bool> ExistColor(string colorName, string colorCode, int colorid);

        int CountInPage();


        #endregion
    }
}
