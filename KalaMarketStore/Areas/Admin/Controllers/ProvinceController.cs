using KalaMarketStore.Core.Service.Province;
using KalaMarketStore.DataLayer.Entities.Address;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProvinceController : Controller
    {
        private readonly IProvinceService ProvinceService;
        public ProvinceController(IProvinceService ProvinceService)
        {
            this.ProvinceService = ProvinceService;
        }


        #region Index
        public IActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            ViewBag.counts = ProvinceService.CountInPage();
            return View(ProvinceService.ShowAllProvince());
        }
        #endregion


        #region Add
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AddProvince");
        }

        [HttpPost]
        public IActionResult Add(Province Province)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ProvinceService.ExistProvince(Province.ProvinceName, 0))
            {
                return Json(5);
            }
            int ProvinceId = ProvinceService.AddProvince(Province);
            int sendjson = ProvinceId > 0 ? 1 : 4;

            return Json(sendjson);


        }
        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return PartialView("_UpdateProvince", ProvinceService.FindProvinceById(id));

        }


        [HttpPost]
        public IActionResult Edit(Province Province)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ProvinceService.ExistProvince(Province.ProvinceName, Province.ProvinceId))
            {
                return Json(5);
            }
            bool ProvinceId = ProvinceService.UpdateProvince(Province);
            int sendjson = ProvinceId ? 2 : 4;

            return Json(sendjson);

        }
        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_DeleteProvince", ProvinceService.FindProvinceById(id));
        }

        [HttpPost]
        public IActionResult Delete(Province Province)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            bool ProvinceId = ProvinceService.DeleteProvince(Province);
            int sendjson = ProvinceId ? 3 : 4;

            return Json(sendjson);
        }
        #endregion
    }
}
