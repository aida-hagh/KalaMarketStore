using KalaMarketStore.Core.Service.Brand;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService brandService;
        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;   
        }
        #region Index
        public IActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            ViewBag.counts = brandService.CountInPage();
            return View(brandService.ShowAllBrand());
        }
        #endregion


        #region Add
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AddBrand");
        }

        [HttpPost]
        public IActionResult Add(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (brandService.ExistBrand(brand.BrandFaName, brand.BrandEnName, 0))
            {
                return Json(5);
            }
            int BrandId = brandService.AddBrand(brand);
            int sendjson = BrandId > 0 ? 1 : 4;

            return Json(sendjson);


        }
        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return PartialView("_UpdateBrand", brandService.FindBrandById(id));

        }


        [HttpPost]
        public IActionResult Edit(Brand Brand)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (brandService.ExistBrand(Brand.BrandFaName, Brand.BrandEnName, Brand.BrandId))
            {
                return Json(5);
            }
            bool BrandId = brandService.UpdateBrand(Brand);
            int sendjson = BrandId ? 2 : 4;

            return Json(sendjson);

        }
        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_DeleteBrand", brandService.FindBrandById(id));
        }

        [HttpPost]
        public IActionResult Delete(Brand Brand)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            bool BrandId = brandService.DeleteBrand(Brand);
            int sendjson = BrandId ? 3 : 4;

            return Json(sendjson);
        }
        #endregion
    }
}
