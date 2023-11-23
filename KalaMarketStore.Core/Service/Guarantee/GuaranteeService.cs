using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Guarantee
{
    public class GuaranteeService : IGuaranteeService
    {
        private readonly AppDbContext context;
        public GuaranteeService(AppDbContext context) 
        {
            this.context = context;
        }

        public int AddGuarantee(ProductGuarantee productGuarantee)
        {
            try
            {
                context.ProductGuarantees.Add(productGuarantee);
                context.SaveChanges();
                return productGuarantee.GuaranteeId;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public bool DeleteGuarantee(ProductGuarantee productGuarantee)
        {
            try
            {
                productGuarantee.IsDelete = true;
                context.ProductGuarantees.Update(productGuarantee);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool ExistGuarantee(string guaranteeName, int guaranteeId)
        {
            return context.ProductGuarantees.Any(x=>x.GuaranteeName==guaranteeName && x.GuaranteeId!=guaranteeId && !x.IsDelete);  
        }

        public ProductGuarantee FindGuaranteeById(int guaranteeId)
        {
           return context.ProductGuarantees.Find(guaranteeId);
        }


        public List<ProductGuarantee> ShowAllGuarantee(int page)
        {
            int skip = (page - 1) * 4;
            return context.ProductGuarantees.Where(x=>!x.IsDelete).Skip(skip).Take(4).ToList(); 
        }  
        
        public List<ProductGuarantee> ShowAllGuarantee()
        {
         
            return context.ProductGuarantees.Where(x=>!x.IsDelete).ToList(); 
        }

        public bool UpdateGuarantee(ProductGuarantee productGuarantee)
        {
            try
            {
                context.ProductGuarantees.Update(productGuarantee);
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
            int count = context.ProductGuarantees.Count();
            if (count % 4 != 0)
            {
                count++;
            }
            count = count / 4;
            return count;
        }
    }
}
