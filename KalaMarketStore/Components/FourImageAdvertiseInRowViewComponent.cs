using KalaMarketStore.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KalaMarketStore.Components
{
    [ViewComponent(Name = "FourImageAdvertise")]
    public class FourImageAdvertiseInRowViewComponent:ViewComponent
    {

        private readonly AppDbContext context;
        public FourImageAdvertiseInRowViewComponent(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var countImg = await context.ImageAdvertises.CountAsync();
            if (countImg > 4)
            {
                var fourImg = await context.ImageAdvertises.OrderBy(x => x.ImageAdvertise_Order).Skip(4).Take(4).ToListAsync();

                return View("/Views/Shared/Components/FourImageAdvertiseInRow/FourImageAdvertiseInRow.cshtml", fourImg);
            }
            else
            {
                return View("/Views/Shared/Components/FourImageAdvertiseInRow/FourImageAdvertiseInRow.cshtml");
            }

        }


    }
}
