//using KalaMarketStore.Core.Service.Category;
//using KalaMarketStore.Core.Service.Product;
//using KalaMarketStore.DataLayer.Context;
//using Microsoft.AspNetCore.Mvc;

//namespace KalaMarketStore.Components

//{

//    [ViewComponent(Name = "ShowMenuViewComponent")]
//    public class ShowMenuViewComponent : ViewComponent
//    {
//        private readonly ICategoryService categoryService;
//        public ShowMenuViewComponent(ICategoryService categoryService)
//        {
//            this.categoryService = categoryService;
//        }


//        public async Task<IViewComponentResult> InvokeAsync()
//        {
                      
//            return await Task.FromResult(View("ShowMenu", categoryService.GetAllCategoryForMenu()));
//        }
//    }
//}
