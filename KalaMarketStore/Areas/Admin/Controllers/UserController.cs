using KalaMarketStore.Core.Service.RolePermission;
using KalaMarketStore.Core.Service.User;
using KalaMarketStore.Core.Service.UserRole;
using Microsoft.AspNetCore.Mvc;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserRoleService userRoleService;
        private readonly IRoleService roleService;
        public UserController(IUserService userService, IUserRoleService userRoleService,
            IRoleService roleService)
        {
            this.userService=userService;  
            this.userRoleService=userRoleService;
            this.roleService=roleService;
        }


        public IActionResult Index()
        {
            ViewData["UserRole"] = userRoleService.ShowUserRole();
            ViewData["Role"] = roleService.ShowAllRole();
            return View(userService.ShowAllUser());
        }
    }
}
