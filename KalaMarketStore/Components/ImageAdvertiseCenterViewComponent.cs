using KalaMarketStore.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace KalaMarketStore.Components
{
    
        [ViewComponent(Name = "ImageAdvertiseCenter")]
        public class ImageAdvertiseCenterViewComponent : ViewComponent
        {
            private readonly AppDbContext context;

            public ImageAdvertiseCenterViewComponent(AppDbContext context)
            {
                this.context = context;
            }
            public async Task<IViewComponentResult> InvokeAsync()
            {
                var countImg = await context.ImageAdvertises.CountAsync();
                if (countImg > 3)
                {
                    var centerImg = await context.ImageAdvertises.OrderBy(x => x.ImageAdvertise_Order).Skip(3).Take(1).FirstOrDefaultAsync();

                    return View("/Views/Shared/Components/ImageAdvertiseCenter/ImageAdvertiseCenter.cshtml", centerImg);
                }
                else
                {
                    return View("/Views/Shared/Components/ImageAdvertiseCenter/ImageAdvertiseCenter.cshtml");
                }

            }



        }
    
}
