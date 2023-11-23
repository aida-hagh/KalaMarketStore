using KalaMarketStore.Core.Service.Permission;
using KalaMarketStore.Core.Service.RolePermission;
using KalaMarketStore.Core.Service.User;
using KalaMarketStore.Core.Service.UserRole;
using KalaMarketStore.DataLayer.Entities.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KalaMarketStore.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class RolePermissionController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IPermissionService permissionService;
        public RolePermissionController( IUserService userService, IRoleService roleService
            , IPermissionService permissionService)
           
        {
            this.userService = userService;
            this.roleService = roleService;
            this.permissionService = permissionService;
        }


        #region Index
        public IActionResult Index()
        {
            return View(roleService.ShowRolePermission());
        }

        #endregion


        #region Add
        [HttpGet]
        public IActionResult Add(int userid)
        {
            //int userId = int.Parse(User.FindFirst("UserId").Value);
            //userid = userId;
        
            ViewBag.Permission = new SelectList(permissionService.ShowAllPermission(), "PermissionId", "PermissionTitle");
            ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
            return View();

       
        }

        [HttpPost]
        public IActionResult Add(RolePermission rolePermission)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (roleService.ExistRolePermission(rolePermission.PermissionId, rolePermission.RoleId, 0))
                    {
                        ModelState.AddModelError("PermissionId", "  این دسترسی برای این نقش قبلا انتخاب شده است.");
                        ViewBag.Permission = new SelectList(permissionService.ShowAllPermission(), "PermissionId", "PermissionTitle");
                        ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
                        return View(rolePermission);
                    }
                    var URid = roleService.AddRolePermission(rolePermission);
                    TempData["Result"] = URid > 0 ? "true" : "false";

                    return Redirect("/Admin/RolePermission/Index");
                }
                //ViewBag.User = new SelectList(userService.FindUserByIdToList(userRole.UserId), "UserId", "UserName", "UserFamily");
                ViewBag.Permission = new SelectList(permissionService.ShowAllPermission(), "PermissionId", "PermissionTitle");
                ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
                return View(rolePermission);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Permission = new SelectList(permissionService.ShowAllPermission(), "PermissionId", "PermissionTitle");
            ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
            var rolePer = roleService.FindRolePermissionById(id);
            return View(rolePer);


        }


        [HttpPost]
        public IActionResult Edit(RolePermission rolePermission)
        {
            if (ModelState.IsValid)
            {
                //var ur = userRoleService.FindUserRoleById(userRole.UserRoleId);

                if (roleService.ExistRolePermission(rolePermission.PermissionId, rolePermission.RoleId, rolePermission.RolePermissionId))
                {
                    ModelState.AddModelError("PermissionId", "  این دسترسی برای این نقش قبلا انتخاب شده است.");
                    ViewBag.Permission = new SelectList(permissionService.ShowAllPermission(), "PermissionId", "PermissionTitle");
                    ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
                    return View(rolePermission);
                }
                var roleperid = roleService.UpdateRolePermission(rolePermission);
                TempData["Result"] = roleperid ? "true" : "false";

                return Redirect("/Admin/RolePermission/Index");
            }
            return View(rolePermission);
        }



        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            var urid = roleService.FindRolePermissionById(id);

            if (urid == null)
                return NotFound();

            return View(urid);
        }

        [HttpPost]
        public IActionResult Delete(RolePermission rolePermission)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var RolePerid = roleService.DeleteRolePermission(rolePermission);
            TempData["Result"] = RolePerid ? "true" : "false";

            return Redirect("/Admin/RolePermission/Index");

        }
        #endregion
    }
}
