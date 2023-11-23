using KalaMarketStore.Core.Service.Category;
using KalaMarketStore.Core.Service.Property;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Products;
using KalaMarketStore.DataLayer.Entities.Products.M_to_M;
using Microsoft.AspNetCore.Mvc;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly ICategoryService categoryService;
        private readonly AppDbContext context;

        public PropertyController(IPropertyService propertyService, AppDbContext context, ICategoryService categoryService)
        {
            this.propertyService = propertyService;
            this.categoryService = categoryService;
            this.context = context;

        }



        #region Index
        public IActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            ViewBag.PropertyCount = propertyService.CountInPage();

            return View(propertyService.ShowAllProperty(page));
        }
        #endregion


        #region Add
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Category = categoryService.ShowAllSubCategory();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Property property, List<int> Categoryid)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Category = categoryService.ShowAllSubCategory();
                return RedirectToAction(nameof(Index));
            }

            if (propertyService.ExistProperty(property.PropertyTitle, 0))
            {
                ModelState.AddModelError("PropertyTitle", "خصوصیات وارد شده تکراری میباشد.");
                return View(property);
            }

            int propertyid = propertyService.AddProperty(property);
            if (propertyid <= 0)
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(Index));
            }

            List<Category_Property> AddCateProper = new List<Category_Property>();
            foreach (var item in Categoryid)
            {
                AddCateProper.Add(new Category_Property
                {
                    CategoryId = item,
                    PropertyId = propertyid,
                });
            }
            bool res = propertyService.AddPropertyForCategory(AddCateProper);
            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(Index));


        }
        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Category = categoryService.ShowAllSubCategory();
            ViewBag.property = propertyService.ShowPropertyForUpdate(id);


            return View(propertyService.FindPropertyById(id));

        }


        [HttpPost]
        public IActionResult Edit(Property property, List<int> CategoryId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = categoryService.ShowAllSubCategory();
                ViewBag.property = propertyService.ShowPropertyForUpdate(property.PropertyId);
            }

            if (propertyService.ExistProperty(property.PropertyTitle, property.PropertyId))
            {
                ModelState.AddModelError("PropertyTitle", "خصوصیات وارد شده تکراری میباشد."); return View(property);
            }

            bool UpdateProp = propertyService.UpdateProperty(property);
            if (!UpdateProp)
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(Index));
            }
            bool daleteProp = propertyService.DeletePropertyForCategory(property.PropertyId);
            if (!daleteProp)
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(Index));
            }

            List<Category_Property> categories = new List<Category_Property>();
            foreach (var item in CategoryId)
            {
                categories.Add(new Category_Property
                {
                    CategoryId = item,
                    PropertyId = property.PropertyId

                });
            }
            bool addPropertyForCategory = propertyService.AddPropertyForCategory(categories);
            TempData["Result"] = addPropertyForCategory ? "true" : "false";
            return RedirectToAction(nameof(Index));

        }
        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {

        
            Property property = propertyService.FindPropertyById(id);

            if (property == null)
                return NotFound();

            return View(property);
        }

        [HttpPost]
        public IActionResult Delete(Property property)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));

            }


            var catePro = context.Category_Properties.Where(x => x.PropertyId == property.PropertyId);
            context.Category_Properties.RemoveRange(catePro);
            context.SaveChanges();
            bool res = propertyService.DeleteProperty(property);

            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
