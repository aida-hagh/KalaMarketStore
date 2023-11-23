using KalaMarketStore.Core.ExtentionMethod;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImageAdvertiseController : Controller
    {
        private readonly AppDbContext context;
        public ImageAdvertiseController(AppDbContext context)
        {
            this.context = context;
        }





        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await context.ImageAdvertises.ToListAsync());
        }
        #endregion




        #region Add
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ImageAdvertise ImageAdvertise, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(ImageAdvertise);
            try
            {
                if (file != null)
                {
                    #region For Img

                    if (file == null)
                    {
                        ModelState.AddModelError("ImageAdvertise_Image", "لطفا یک تصویر انتخاب کنید.");
                        return View(ImageAdvertise);
                    }
                    string imgName = Uploudimage.CreateImage(file);
                    if (imgName == "false")
                    {
                        TempData["Result"] = "false";
                        return RedirectToAction(nameof(Index));
                    }
                    ImageAdvertise.ImageAdvertise_Image = imgName;

                    #endregion
                }

                if (ImageAdvertise != null)
                {
                    await context.ImageAdvertises.AddAsync(ImageAdvertise);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(ImageAdvertise);
            }
            catch (Exception)
            {

                throw;
            }



        }
        #endregion




        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            ImageAdvertise ImageAdvertise = await context.ImageAdvertises.FindAsync(id);

            if (ImageAdvertise == null)
            {
                TempData["NotFoundSlider"] = "true";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ImageAdvertise);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit(ImageAdvertise ImageAdvertise, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return View(ImageAdvertise);

            try
            {
                if (file != null)
                {
                    string imgName = Uploudimage.CreateImage(file);
                    if (imgName == "false")
                    {
                        TempData["Result"] = "false";
                        return RedirectToAction(nameof(Index));
                    }
                    bool deleteImg = Uploudimage.DeleteImage("images", ImageAdvertise.ImageAdvertise_Image);
                    if (!deleteImg)
                    {
                        TempData["Result"] = "false";
                        return RedirectToAction(nameof(Index));
                    }
                    ImageAdvertise.ImageAdvertise_Image = imgName;
                }

                if (ImageAdvertise != null)
                {
                    context.ImageAdvertises.Update(ImageAdvertise);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(ImageAdvertise);
            }
            catch (Exception)
            {

                throw;
            }





        }
        #endregion




        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ImageAdvertise ImageAdvertise = await context.ImageAdvertises.FindAsync(id);
            if (ImageAdvertise == null)
            {
                TempData["NotFoundSlider"] = "true";
                return RedirectToAction(nameof(Index));
            }
            return View(ImageAdvertise);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ImageAdvertise ImageAdvertise)
        {

            //bool deleteImg = Uploudimage.DeleteImage("images", ImageAdvertise.ImageAdvertise_Image);
            //if (!deleteImg)
            //{
            //    TempData["Result"] = "false";
            //    return RedirectToAction(nameof(Index));
            //}

            context.ImageAdvertises.Remove(ImageAdvertise);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
