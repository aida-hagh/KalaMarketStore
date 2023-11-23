using KalaMarketStore.Core.Service.Guarantee;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GuaranteeController : Controller
    {
        private readonly IGuaranteeService guaranteeService;
        public GuaranteeController(IGuaranteeService guaranteeService) 
        { 
            this.guaranteeService = guaranteeService;   
        }



        #region Index
        public IActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            ViewBag.countGuarantee = guaranteeService.CountInPage();
            return View(guaranteeService.ShowAllGuarantee(page));
        }
        #endregion


        #region Add
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AddGuarantee");
        }

        [HttpPost]
        public IActionResult Add( ProductGuarantee productGuarantee )
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (guaranteeService.ExistGuarantee(productGuarantee.GuaranteeName,0))
            {
                return Json(5);
            }
           int guaranteeId= guaranteeService.AddGuarantee(productGuarantee);
            int sendjson = guaranteeId > 0 ? 1 : 4;

            return Json(sendjson);
            

        }
        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return PartialView("_UpdateGuarantee", guaranteeService.FindGuaranteeById(id));

        }


        [HttpPost]
        public IActionResult Edit(ProductGuarantee productGuarantee)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (guaranteeService.ExistGuarantee(productGuarantee.GuaranteeName, productGuarantee.GuaranteeId))
            {
                return Json(5);
            }
            bool guaranteeId = guaranteeService.UpdateGuarantee(productGuarantee);
            int sendjson = guaranteeId  ? 2 : 4;

            return Json(sendjson);

        }
        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_DeleteGuarantee", guaranteeService.FindGuaranteeById(id));
        }

        [HttpPost]
        public IActionResult Delete(ProductGuarantee productGuarantee)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            bool guaranteeId = guaranteeService.DeleteGuarantee(productGuarantee);
            int sendjson = guaranteeId ? 3 : 4;

            return Json(sendjson);
        }
        #endregion
    }
}
