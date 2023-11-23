using KalaMarketStore.Core.Service.MainSlider;
using KalaMarketStore.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KalaMarketStore.Components
{
    [ViewComponent(Name = "TwoImageAdvertise")]
    public class TwoImageAdvertiseInTopViewComponent : ViewComponent
    {
        private readonly AppDbContext context;

        public TwoImageAdvertiseInTopViewComponent(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var TopImg = await context.ImageAdvertises.OrderBy(x => x.ImageAdvertise_Order).Skip(0).Take(2).ToListAsync();

            return View("/Views/Shared/Components/ImageAdvertiseInTop/TwoImageAdvertiseInTop.cshtml", TopImg);
        }


    }
}
