using KalaMarketStore.Core.ExtentionMethod;
using KalaMarketStore.Core.Service.MainSlider;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IMainSliderService mainSliderService;
        public SliderController(IMainSliderService mainSliderService)
        {
            this.mainSliderService = mainSliderService; 
        }




        #region Index
        public async Task< IActionResult> Index(int page=1)
        {
            ViewBag.Page = page;
            ViewBag.countSlider = mainSliderService.SliderCount();
            return View(await mainSliderService.ShowAllSlider(page));
        }
        #endregion



        
        #region Add
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MainSlider mainSlider,IFormFile file)
        {
            if(!ModelState.IsValid)
                return View(mainSlider);


            #region For Img

            if (file == null)
            {
                ModelState.AddModelError("SliderImage", "لطفا یک تصویر برای اسلایدر انتخاب کنید.");
                return View(mainSlider);
            }
            string imgName = Uploudimage.CreateImage(file);
            if (imgName== "false")
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(Index));
            }
            mainSlider.SliderImage = imgName;

            #endregion


            int res =await mainSliderService.AddSlider(mainSlider);
            TempData["Result"] = res > 0 ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }
        #endregion




        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            MainSlider mainSlider =await mainSliderService.FindSliderById(id);

            if(mainSlider == null)
            {
                TempData["NotFoundSlider"] = "true";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(mainSlider);
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> Edit(MainSlider mainSlider, IFormFile? file)
        {
            if(!ModelState.IsValid)
            return View(mainSlider);

            if (file!=null)
            {
                string imgName = Uploudimage.CreateImage(file);
                if (imgName == "false")
                {
                    TempData["Result"] = "false";
                    return RedirectToAction(nameof(Index));
                }
                bool deleteImg = Uploudimage.DeleteImage("images", mainSlider.SliderImage);
                if (!deleteImg)
                {
                    TempData["Result"] = "false";
                    return RedirectToAction(nameof(Index));
                }
                mainSlider.SliderImage = imgName;
            }

            bool res= await mainSliderService.UpdateSlider(mainSlider);
            TempData["Result"] = res? "true" : "false";
            return RedirectToAction(nameof(Index));

        }
        #endregion




        #region Delete
        [HttpGet]
        public async Task< IActionResult> Delete(int id)
        {
            MainSlider mainSlider =await mainSliderService.FindSliderById(id);
            if(mainSlider == null)
            {
                TempData["NotFoundSlider"] = "true";
                return RedirectToAction(nameof(Index));
            }
            return View(mainSlider);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MainSlider mainSlider)
        {
            //bool deleteImg = Uploudimage.DeleteImage("images", mainSlider.SliderImage);
            //if (!deleteImg)
            //{
            //    TempData["Result"] = "false";
            //    return RedirectToAction(nameof(Index));
            //}

            bool res =await mainSliderService.DeleteSlider(mainSlider);
            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}
