using KalaMarketStore.Core.Service.MainSlider;
using KalaMarketStore.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KalaMarketStore.Components
{
    [ViewComponent(Name = "ShowSliderUser")]
  
    public class ShowSliderViewComponent:ViewComponent
    {
        private readonly IMainSliderService mainSliderService;
        public ShowSliderViewComponent(IMainSliderService mainSliderService)
        {
            this.mainSliderService = mainSliderService;
        }



        public async Task <IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View("ShowSliderForUser", mainSliderService.ShowSliderForUsers()));
            //var sliders = await context.MainSliders.Where(x => x.IsActive).ToListAsync();
            //return View("/Views/Components/ShowSliderViewComponent/ShowSliderForUser.cshtml", sliders);
        }



    }
}
