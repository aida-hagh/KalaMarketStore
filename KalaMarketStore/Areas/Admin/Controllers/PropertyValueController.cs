using KalaMarketStore.Core.Service.Category;
using KalaMarketStore.Core.Service.Product;
using KalaMarketStore.Core.Service.Property;
using KalaMarketStore.Core.Service.PropertyValue;
using KalaMarketStore.DataLayer.Entities.Products;
using KalaMarketStore.DataLayer.Entities.Products.M_to_M;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KalaMarketStore.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class PropertyValueController : Controller
    {
        private readonly IPropertyValueService propertyValueService; 
        private readonly IPropertyService propertyService; 
        private readonly IProductService productService; 
        public PropertyValueController(IPropertyValueService propertyValueService, IProductService productService, IPropertyService propertyService)
        {

            this.propertyValueService = propertyValueService;
            this.propertyService = propertyService;
            this.productService= productService;


        }

        #region Index
        public IActionResult Index()
        {
           ViewData["Product"] = productService.ShowAllProduct();
            ViewData["Property"] = propertyService.ShowAllProperty();

            return View(propertyValueService.ShowAllPropertyValue());
        }
        #endregion


    


    }
}
