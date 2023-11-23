using KalaMarketStore.Core.ExtentionMethod;
using KalaMarketStore.Core.Service.Brand;
using KalaMarketStore.Core.Service.Category;
using KalaMarketStore.Core.Service.Color;
using KalaMarketStore.Core.Service.Guarantee;
using KalaMarketStore.Core.Service.Product;
using KalaMarketStore.DataLayer.Entities;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly IColorService colorService;
        private readonly IGuaranteeService guaranteeService;

        public ProductController(IProductService productService,
            IBrandService brandService, ICategoryService categoryService, IColorService colorService
            , IGuaranteeService guaranteeService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.brandService = brandService;
            this.colorService = colorService;
            this.guaranteeService = guaranteeService;
        }


        #region Index
        public IActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            ViewBag.counts = productService.CountInPage();
            return View(productService.ShowAllProduct(page));
        }
        #endregion


        #region Add
        [HttpGet]
        public IActionResult Add(int page)
        {
            ViewBag.Category = new SelectList(categoryService.ShowMainCategory(), "CategoryId", "CategoryFaTitle");
            ViewBag.Brand = new SelectList(brandService.ShowAllBrand(), "BrandId", "BrandFaName");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product, IFormFile file, int page)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = new SelectList(categoryService.ShowMainCategory(), "CategoryId", "CategoryFaTitle");
                ViewBag.Brand = new SelectList(brandService.ShowAllBrand(), "BrandId", "BrandFaName");
                return View(product);
            }

            #region For Img

            if (file == null)
            {
                ModelState.AddModelError("SliderImage", "لطفا یک تصویر برای اسلایدر انتخاب کنید.");
                return View(product);
            }
            string imgName = Uploudimage.CreateImage(file);
            if (imgName == "false")
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(Index));
            }
            product.ProductCreate = DateTime.Now;
            product.ProductImage = imgName;

            #endregion

            int productid = productService.AddProduct(product);
            TempData["Result"] = productid > 0 ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }


        #endregion


        #region Edit
       [HttpGet]
        public IActionResult Edit(int id,int subCategoryId)
        {
            Product update = productService.FindProductById(id);
            if (update == null)
            {
                return NotFound();
            }
            ViewBag.Category = new SelectList(categoryService.ShowMainCategory(), "CategoryId", "CategoryFaTitle");
            //ViewBag.AllCategory =categoryService.ShowAllCategory();
            ViewBag.Brand = new SelectList(brandService.ShowAllBrand(), "BrandId", "BrandFaName");
            return View(update);


        }


        [HttpPost]
        public IActionResult Edit(Product product, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                 ViewBag.Category = new SelectList(categoryService.ShowMainCategory(), "CategoryId", "CategoryFaTitle");
               // ViewBag.AllCategory = categoryService.ShowAllCategory();

                ViewBag.Brand = new SelectList(brandService.ShowAllBrand(), "BrandId", "BrandFaName");
                return View(product);
            }
            if (file != null)
            {
                string imgName = Uploudimage.CreateImage(file);
                if (imgName == "false")
                {
                    TempData["Result"] = "false";
                    return RedirectToAction(nameof(Index));
                }
                bool deleteImg = Uploudimage.DeleteImage("images", product.ProductImage);
                if (!deleteImg)
                {
                    TempData["Result"] = "false";
                    return RedirectToAction(nameof(Index));
                }
                product.ProductImage = imgName;
            }
            
            product.ProductUpdate = DateTime.Now;
            product.ProductCreate = DateTime.Now;
            bool update = productService.UpdateProduct(product);
            TempData["Result"] = update ? "true" : "false";
            return RedirectToAction(nameof(Index));

        }
        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {

            Product deletePro = productService.FindProductById(id);
            if (deletePro == null)
            {
                return NotFound();
            }

            return View(deletePro);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {

            bool deleteImg = Uploudimage.DeleteImage("images", product.ProductImage);
            if (!deleteImg)
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(Index));
            }
            bool res = productService.DeleteProduct(product);

            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region PropertyForProduct
        public IActionResult IndexPropertyForProduct(int id)
        {
            int categoryid = productService.FindCategoryForProduct(id);
            ViewBag.Propertys = productService.ShowAllPropertyForCategory(categoryid);
            ViewBag.PropertyValue = productService.ShowPropertyValueForProduct(id);
            TempData["productid"] = id;
            return View();
        }

        public IActionResult AddPropertyForProduct(List<int> nameid, List<string> value)
        {
            int productid = int.Parse(TempData["productid"].ToString());
            if (nameid.Count != value.Count)
            {
                int categoryid = productService.FindCategoryForProduct(productid);
                ViewBag.Propertys = productService.ShowAllPropertyForCategory(categoryid);
                ViewBag.PropertyValue = productService.ShowPropertyValueForProduct(productid);

                TempData["productid"] = productid;
                return View("IndexPropertyForProduct");
            }

            List<PropertyValue> propertyValue = new List<PropertyValue>();

            for (int i = 0; i < nameid.Count; i++)
            {
                propertyValue.Add(new PropertyValue
                {
                    ProductId = productid,
                    propertyValue = value[i],
                    PropertyId = nameid[i]

                });
            }
            List<PropertyValue> _propertyValues = propertyValue.Where(x => x.propertyValue != null).ToList();
            bool res = productService.AddOrUpdatePropertyForProduct(_propertyValues, productid);
            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }
        #endregion





        #region Price

        public IActionResult ShowPrice(int id)
        {
            ViewBag.id = id;
            return View(productService.ShowAllPriceForProduct(id));
        }




        [HttpGet]
        public async Task<IActionResult> AddPrice(int id)
        {
            ViewBag.Color = new SelectList(await colorService.ShowAllColor(), "ColorId", "ColorName");
            ViewBag.Guarantee = new SelectList(guaranteeService.ShowAllGuarantee(), "GuaranteeId", "GuaranteeName");
            ViewBag.id = id;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddPrice(ProductPrice productPrice)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Color = new SelectList(await colorService.ShowAllColor(), "ColorId", "ColorName");
                ViewBag.Guarantee = new SelectList(guaranteeService.ShowAllGuarantee(), "GuaranteeId", "GuaranteeName");
                ViewBag.id = productPrice.ProductId;
                return View(productPrice);
            }
            productPrice.CreateDate = DateTime.Now;
            int productId = productService.AddPriceForeProduct(productPrice);
            TempData["Result"] = productId > 0 ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> EditPrice(int id)
        {
            ProductPrice productPrice = productService.FindPriceById(id);
            if (productPrice==null)
            {
                NotFound();
            }
            ViewBag.Color = new SelectList(await colorService.ShowAllColor(), "ColorId", "ColorName");
            ViewBag.Guarantee = new SelectList(guaranteeService.ShowAllGuarantee(), "GuaranteeId", "GuaranteeName");
            ViewBag.id = productPrice.ProductId;
            return View(productPrice);
        }

        [HttpPost]
        public async Task<IActionResult> EditPrice(ProductPrice productPrice)
        { 

            if (!ModelState.IsValid)
            {
                ViewBag.Color = new SelectList(await colorService.ShowAllColor(), "ColorId", "ColorName");
                ViewBag.Guarantee = new SelectList(guaranteeService.ShowAllGuarantee(), "GuaranteeId", "GuaranteeName");
                ViewBag.id = productPrice.ProductId;
                return View(productPrice);
            }

            productPrice.CreateDate= DateTime.Now;  
            bool price =productService.UpdatePrice(productPrice);
            TempData["Result"] = price ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult DeletePrice(int id)
        {
            ProductPrice productPrice = productService.FindPriceById(id);
            if (productPrice==null)
            {
                return NotFound();
            }
            return View(productPrice);

        }    
        
        
        
        [HttpGet]
        public IActionResult DeletePrice(ProductPrice productPrice)
        {
            bool res = productService.DeleteProductPrice(productPrice);
            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(Index));

        }


            #endregion





        }
}
