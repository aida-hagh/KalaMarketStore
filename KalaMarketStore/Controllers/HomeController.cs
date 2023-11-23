using KalaMarketStore.Core.Service.Product;
using KalaMarketStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KalaMarketStore.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        public HomeController(IProductService productService)
        {
            this.productService = productService;   
        }
        public IActionResult Index()
        {
            ViewBag.SpecialPrice = productService.ShowAllSpecialProduct();
            //var catepro = productService.ShowProductForCategory(id);
            return View();
        }

       
    }
}