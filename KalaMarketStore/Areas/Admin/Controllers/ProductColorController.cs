using KalaMarketStore.Core.ExtentionMethod;
using KalaMarketStore.Core.Service.Color;
using KalaMarketStore.Core.Service.MainSlider;
using KalaMarketStore.DataLayer.Entities;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductColorController : Controller
    {
        private IColorService ColorService;
        public ProductColorController(IColorService ColorService)
        {
            this.ColorService = ColorService;
        }




        #region Index
        public async Task<IActionResult> Index(int page=1)
        {
            ViewBag.Page = page;
            ViewBag.countColor = ColorService.CountInPage();
            return View(await ColorService.ShowAllColor(page));
        }
        #endregion




        #region Add
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductColor productColor)
        {
            if (!ModelState.IsValid)
                return View(productColor);
            if(await ColorService.ExistColor(productColor.ColorName, productColor.ColorCode,0))
            {
                ModelState.AddModelError("ErrorColor","رنگ انتخابی تکراری است.");
                return View(productColor);  
            }

            int colorid= await ColorService.AddColor(productColor);
            TempData["Result"] = colorid > 0 ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }

        #endregion




        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int colorid)
        {
            if (colorid == 0)
                return NotFound();

            ProductColor productColor =await ColorService.FindColorById(colorid);

            if (productColor == null)
            {
                return NotFound();  
            }
            return View(productColor);

        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductColor productColor)
        {
            
            
                if (!ModelState.IsValid)
                    return View(productColor);
                if (await ColorService.ExistColor(productColor.ColorName, productColor.ColorCode,productColor.ColorId))
                {
                    ModelState.AddModelError("ErrorColor", "رنگ انتخابی تکراری است.");
                    return View(productColor);
                }
                bool res=await ColorService.UpdateColor(productColor);
                TempData["Result"] = res ? "true" : "false";
                return RedirectToAction(nameof(Index));
         
            

        }
        #endregion


   
    }
}
