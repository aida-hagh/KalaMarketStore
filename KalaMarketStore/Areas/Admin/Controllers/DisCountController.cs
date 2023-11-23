using KalaMarketStore.Core.ExtentionMethod;
using KalaMarketStore.Core.Service.Brand;
using KalaMarketStore.Core.Service.Category;
using KalaMarketStore.Core.Service.DisCount;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities;
using KalaMarketStore.DataLayer.Entities.OfferCodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DisCountController : Controller
    {
        private readonly IDisCountService disCountService;
        private readonly AppDbContext context;
        public DisCountController(IDisCountService disCountService, AppDbContext context)
        {
            this.disCountService = disCountService;
            this.context = context;
        }




        #region Index
        public IActionResult Index()
        {

            return View(context.DisCounts.ToList());
        }
        #endregion


        #region Add
        [HttpGet]
        public IActionResult Add(int page)
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(DisCount DisCount)
        {
            if (ModelState.IsValid)
            {
                context.DisCounts.Add(DisCount);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }

            return View(DisCount);

        }
        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var disid = context.DisCounts.FirstOrDefault(x => x.DisCountId == id);
            if (disid == null)
            {
                return NotFound();
            }

            return View(disid);
        }


        [HttpPost]
        public IActionResult Edit(DisCount DisCount)
        {
            var disid = context.DisCounts.FirstOrDefault(x => x.DisCountId == DisCount.DisCountId);
            if (ModelState.IsValid)
            {
                context.DisCounts.Update(disid);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }


            return View(DisCount);



        }
        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {

            var disid = context.DisCounts.FirstOrDefault(x => x.DisCountId == id);
            if (disid == null)
            {
                return NotFound();
            }

            return View(disid);
        }

        [HttpPost]
        public IActionResult Delete(DisCount DisCount)
        {
            try
            {
                context.DisCounts.Remove(DisCount);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }


        }
        #endregion
    }
}
