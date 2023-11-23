using KalaMarketStore.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KalaMarketStore.Components
{
    [ViewComponent(Name = "ImageAdvertiseRight")]
    public class ImageAdvertiseInRightViewComponent : ViewComponent
    {
        private readonly AppDbContext context;

        public ImageAdvertiseInRightViewComponent(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var countImg = await context.ImageAdvertises.CountAsync();
            if (countImg > 2)
            {
                var rightImg = await context.ImageAdvertises.OrderBy(x => x.ImageAdvertise_Order).Skip(2).Take(1).FirstOrDefaultAsync();

                return View("/Views/Shared/Components/ImageAdvertiseInRight/ImageAdvertiseInRight.cshtml", rightImg);
            }
            else
            {
                return View("/Views/Shared/Components/ImageAdvertiseInRight/ImageAdvertiseInRight.cshtml");
            }

        }



    }
}
