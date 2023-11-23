using KalaMarketStore.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext context;
        public CategoryService(AppDbContext context)
        {
            this.context = context;
        }


        public int AddCategory(DataLayer.Entities.Products.Category category)
        {
            try
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return category.CategoryId;
            }
            catch (Exception)
            {

                return 0;
            }

        }


        public bool DeleteCategory(DataLayer.Entities.Products.Category category)
        {
            try
            {
                context.Categories.Update(category);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public DataLayer.Entities.Products.Category FindCategoryById(int categiryid)
        {
            return context.Categories.AsNoTracking().FirstOrDefault(x => x.CategoryId == categiryid);

        }


        public List<DataLayer.Entities.Products.Category> ShowMainCategory()
        {
            return context.Categories.Where(x => !x.IsDelete && x.SubCategiry == null).ToList();
        }
        public List<DataLayer.Entities.Products.Category> ShowAllCategory()
        {
            return context.Categories.Where(x => !x.IsDelete).ToList();
        }

        public List<DataLayer.Entities.Products.Category> ShowSubCategory(int id)
        {
            return context.Categories.Where(x => !x.IsDelete && x.SubCategiry == id).ToList();
        }


        public bool UpdateCategory(DataLayer.Entities.Products.Category category)
        {
            try
            {
                context.Categories.Update(category);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string res = ex.Message;
                return false;
            }

        }


        public bool ExistCategory(string faTitle, string enTitle, int categiryid)
        {
            return context.Categories.Any(x => x.CategoryFaTitle == faTitle && x.CategoryEnTitle == enTitle && x.CategoryId != categiryid && !x.IsDelete);
        }


        public IEnumerable<DataLayer.Entities.Products.Category> ShowAllSubCategory()
        {
            return context.Categories.Where(x => x.SubCategiry != null && !x.IsDelete).ToList();
        }


        public List<DataLayer.Entities.Products.Category> GetAllCategoryForMenu()
        {
            return context.Categories.Where(x => !x.IsDelete).ToList();
        }
    }
}
