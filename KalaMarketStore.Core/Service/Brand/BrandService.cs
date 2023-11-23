using KalaMarketStore.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Brand
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext context;
        public BrandService(AppDbContext context) 
        {
            this.context = context;
        }

        public int AddBrand(DataLayer.Entities.Products.Brand brand)
        {
            try
            {
                context.Brands.Add(brand);
                context.SaveChanges();
                return brand.BrandId;
            }
            catch (Exception)
            {

                return 0;
            }
        }

    
        public bool DeleteBrand(DataLayer.Entities.Products.Brand brand)
        {
            try
            {
                brand.IsDelete = true;
                context.Brands.Update(brand);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool ExistBrand(string FaName,string EnName, int BrandId)
        {
            return context.Brands.Any(x=>x.BrandFaName== FaName || x.BrandEnName== EnName && x.BrandId!=BrandId && !x.IsDelete);
        }


        public DataLayer.Entities.Products.Brand FindBrandById(int brandId)
        {
            return context.Brands.Find(brandId);
        }


        public List<DataLayer.Entities.Products.Brand> ShowAllBrand(int page)
        {
            int skip = (page - 1) * 4;
            return context.Brands.Where(x => !x.IsDelete).Skip(skip).Take(4).ToList();
        }  
        public IEnumerable<DataLayer.Entities.Products.Brand> ShowAllBrand()
        {
        
            return context.Brands.Where(x => !x.IsDelete).ToList();
        }


        public bool UpdateBrand(DataLayer.Entities.Products.Brand brand)
        {
            try
            {
                context.Brands.Update(brand);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public int CountInPage()
        {
            int count = context.Brands.Count();
            if (count % 4 != 0)
            {
                count++;
            }
            count = count / 4;
            return count;
        }

    
    }

}
