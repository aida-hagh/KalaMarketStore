using KalaMarketStore.Core.Service.Product;
using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace KalaMarketStore.Components
{
    [ViewComponent(Name = "ShowProductSlider")]

    public class ProductSliderForCategoryViewComponent : ViewComponent
    {

        private readonly IProductService productService;
        private readonly AppDbContext context;
        public ProductSliderForCategoryViewComponent(IProductService productService, AppDbContext context)
        {
            this.productService = productService;
            this.context = context;
        }
        //public async Task<IViewComponentResult> InvokeAsync(int id)
        //{
        //    return await Task.FromResult(View("ProductSliderForCategory", productService.ShowSliderProductForCategory(id)));
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var cate = await context.Categories.Include(x => x.Products).Where(x => x.SubCategiry == null).Select(x => new ProductSliderForCategoryViewModel()
            {
                CategoryTitle = x.CategoryFaTitle,
                CategoryId = x.CategoryId,

                ProductViewModels = (from pr in context.ProductPrices
                                     join pro in context.Products
                                     on pr.ProductId equals pro.ProductId
                                     join _cate in context.Categories
                                     on pro.CategoryId equals _cate.CategoryId
                                     where _cate.SubCategiry == null

                                     select new ProductViewModel
                                     {
                                         MainPrice = pr.MainPrice,
                                         SpecialPrice = pr.SpecialPrice,
                                         ProductFaTitle = pro.ProductFaTitle,
                                         ProductImage = pro.ProductImage,
                                         ProductId = pro.ProductId,
                                         CategoryId = _cate.CategoryId,
                                     }).ToList(),

            }).ToListAsync();


            //return await Task.FromResult(View("ProductSliderForCategory", cate));
            return View("/Views/Shared/Components/ShowProductSlider/ProductSliderForCategory.cshtml", cate);


        }

    }
}