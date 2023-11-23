using KalaMarketStore.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Category
{
    public interface ICategoryService
    {

        List<DataLayer.Entities.Products.Category> ShowMainCategory();
        List<DataLayer.Entities.Products.Category> ShowAllCategory();
        int AddCategory(DataLayer.Entities.Products.Category category); 

        bool UpdateCategory(DataLayer.Entities.Products.Category category); 

        bool DeleteCategory(DataLayer.Entities.Products.Category category);

        List<DataLayer.Entities.Products.Category> ShowSubCategory(int id);

        DataLayer.Entities.Products.Category FindCategoryById(int categiryid);

        bool ExistCategory(string faTitle, string enTitle, int categiryid);

       IEnumerable<DataLayer.Entities.Products.Category> ShowAllSubCategory();

        List<DataLayer.Entities.Products.Category> GetAllCategoryForMenu();

    }

}
