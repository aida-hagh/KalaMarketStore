using Kalamarket.Core.Security;
using KalaMarketStore.Core.Service.Cart;
using Microsoft.AspNetCore.Mvc;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
  
    public class DashbordAdminController : Controller
    {

        private ICartService cartService;
        public DashbordAdminController(ICartService cartService) 
        { 
            this.cartService = cartService; 
        }

        //[CheckPermission(1)]
        public IActionResult Index()
        {
            ViewBag.chart = cartService.highchart();
            return View();
        }
    }
}
