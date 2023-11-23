using KalaMarketStore.Core.ExtentionMethod;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        private readonly AppDbContext context;
        public GalleryController(AppDbContext context)
        {
            this.context = context;
        }



        public IActionResult Index(int id)
        {
            ViewBag.id = id;
            return View(context.ProductGalleries.Where(x => x.ProductId == id).ToList());
        }


        [HttpGet]
        public IActionResult Add(int id)
        {
            ViewBag.Product = new SelectList(context.Products.ToList(), "ProductId", "ProductFaTitle");
            ViewBag.id = id;
            return View();
        }


        [HttpPost]
        public IActionResult Add(ProductGallery productGallery, IFormFile file, int id)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.id = id;
                ViewBag.Product = new SelectList(context.Products.ToList(), "ProductId", "ProductFaTitle");
                return View(productGallery);
            }

            #region For Img

            if (file == null)
            {
                ModelState.AddModelError("SliderImage", "لطفا یک تصویر برای گالری محصول انتخاب کنید.");
                return View(productGallery);
            }
            string img = Uploudimage.CreateImage(file);
            if (img == "false")
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(Index));
            }
            productGallery.ImgName = img;

            #endregion

            context.ProductGalleries.Add(productGallery);
            context.SaveChanges();
            int galleryid = productGallery.ProductGalleryId;
            TempData["Result"] = galleryid > 0 ? "true" : "false";


            return Redirect("/Admin/Product/Index");

        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            var gallery = context.ProductGalleries.FirstOrDefault(x => x.ProductId == id);
            if (gallery == null)
            {
                NotFound();
            }
            ViewBag.Product = new SelectList(context.Products.ToList(), "ProductId", "ProductFaTitle");
            ViewBag.id = gallery.ProductId;
            return View(gallery);
        }


        [HttpPost]
        public IActionResult Edit(ProductGallery productGallery, IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Product = new SelectList(context.Products.ToList(), "ProductId", "ProductFaTitle");
                    ViewBag.id = productGallery.ProductId;
                    return View(productGallery);
                }

                if (file != null)
                {
                    string img = Uploudimage.CreateImage(file);
                    if (img == "false")
                    {
                        TempData["Result"] = "false";
                        return RedirectToAction(nameof(Index));
                    }
                    bool deleteImg = Uploudimage.DeleteImage("images", productGallery.ImgName);
                    if (!deleteImg)
                    {
                        TempData["Result"] = "false";
                        return RedirectToAction(nameof(Index));
                    }
                    productGallery.ImgName = img;
                }

                context.ProductGalleries.Update(productGallery);
                context.SaveChanges();
                return Redirect("/Admin/Product/Index");
            }
            catch (Exception)
            {

                throw;
            }




        }










    }
}
