using KalaMarketStore.Core.Service.City;
using KalaMarketStore.Core.Service.Province;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Address;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CityController : Controller
    {
        private readonly AppDbContext context;
        private readonly ICityService CityService;
        private readonly IProvinceService ProvinceService;
        public CityController(ICityService CityService, IProvinceService ProvinceService,
            AppDbContext context)
        {
            this.CityService = CityService;
            this.ProvinceService = ProvinceService;
            this.context = context;
        }


        #region Index
        public IActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            ViewBag.counts = CityService.CountInPage();
            return View(context.Cities.Where(x => !x.IsDelete).Include(x => x.Province).ToList());
        }
        #endregion


        #region Add
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Province = new SelectList(ProvinceService.ShowAllProvince(), "ProvinceId", "ProvinceName");

            return View();
        }

        [HttpPost]
        public IActionResult Add(City City)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Province = new SelectList(ProvinceService.ShowAllProvince(), "ProvinceId", "ProvinceName", City.ProvinceId);

                return RedirectToAction(nameof(Index));
            }

            if (CityService.ExistCity(City.CityName, 0))
            {
                ModelState.AddModelError("CategoryEnTitle", " شهر تکراری است.");
                return View(City);
            }

            int CityId = CityService.AddCity(City);

            TempData["Result"] = CityId > 0 ? "true" : "false";
            return RedirectToAction(nameof(Index));


        }
        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Province = new SelectList(ProvinceService.ShowAllProvince(), "ProvinceId", "ProvinceName");

            return View(CityService.FindCityById(id));

        }


        [HttpPost]
        public IActionResult Edit(City City)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Province = new SelectList(ProvinceService.ShowAllProvince(), "ProvinceId", "ProvinceName");

                return RedirectToAction(nameof(Index));
            }

            if (CityService.ExistCity(City.CityName, City.CityId))
            {
                ModelState.AddModelError("CategoryEnTitle", " شهر تکراری است.");
                return View(City);
            }
            bool CityId = CityService.UpdateCity(City);

            TempData["Result"] = CityId ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            City cityid = CityService.FindCityById(id);
            if (cityid == null)
            {
                return NotFound();
            }
            return View(cityid);
        }

        [HttpPost]
        public IActionResult Delete(City City)
        {
            City cityid = CityService.FindCityById(City.CityId);

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            bool CityId = CityService.DeleteCity(cityid);

            TempData["Result"] = CityId ? "true" : "false";
            return RedirectToAction(nameof(Index));

        }
        #endregion

    }
}