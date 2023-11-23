using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Color
{
    public class ColorService : IColorService
    {
        private readonly AppDbContext Context;
        public ColorService(AppDbContext Context) 
        { 
            this.Context = Context;
        }



        #region ProductColor
        public async Task<int> AddColor(ProductColor productColor)
        {
            try
            {
                await Context.ProductColors.AddAsync(productColor);
                await Context.SaveChangesAsync();
                return productColor.ColorId;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public async Task<bool> ExistColor(string colorName, string colorCode, int colorid)
        {
            return await Context.ProductColors.AnyAsync(x => x.ColorName == colorName && x.ColorCode == colorCode && x.ColorId != colorid);
        }

        public async Task<ProductColor> FindColorById(int colorid)
        {


            return await Context.ProductColors.FindAsync(colorid);

        }

        public async Task<IEnumerable<ProductColor>> ShowAllColor(int page)
        {
            int skip = (page - 1) * 4;
            return await Context.ProductColors.Skip(skip).Take(4).ToListAsync();

        }
        public async Task<List<ProductColor>> ShowAllColor()
        {
           
            return await Context.ProductColors.ToListAsync();

        }

        public async Task<bool> UpdateColor(ProductColor productColor)
        {
            try
            {
                Context.ProductColors.Update(productColor);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public int CountInPage()
        {
            int count = Context.ProductColors.Count();
            if (count % 4 != 0)
            {
                count++;
            }
            count = count / 4;
            return count;
        }

        #endregion
    }
}
