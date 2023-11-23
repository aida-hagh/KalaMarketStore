using KalaMarketStore.Core.Service.Permission;
using KalaMarketStore.DataLayer.Entities.Roles;
using Microsoft.AspNetCore.Mvc;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService PermissionService;
        public PermissionController(IPermissionService PermissionService)
        {
            this.PermissionService = PermissionService;
        }



        #region Index
        public IActionResult Index()
        {

            return View(PermissionService.ShowAllPermission());
        }
        #endregion


        #region Add
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(Permission Permission)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction(nameof(Index));
            }

            if (PermissionService.ExistPermission(Permission.PermissionTitle, 0))
            {
                ModelState.AddModelError("CategoryEnTitle", "  تکراری است.");
                return View(Permission);
            }

            int PermissionId = PermissionService.AddPermission(Permission);

            TempData["Result"] = PermissionId > 0 ? "true" : "false";
            return RedirectToAction(nameof(Index));


        }
        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {

            return View(PermissionService.FindPermissionById(id));

        }


        [HttpPost]
        public IActionResult Edit(Permission Permission)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction(nameof(Index));
            }

            if (PermissionService.ExistPermission(Permission.PermissionTitle, Permission.PermissionId))
            {
                ModelState.AddModelError("CategoryEnTitle", "  تکراری است.");
                return View(Permission);
            }
            bool PermissionId = PermissionService.UpdatePermission(Permission);

            TempData["Result"] = PermissionId ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Permission Permissionid = PermissionService.FindPermissionById(id);
            if (Permissionid == null)
            {
                return NotFound();
            }
            return View(Permissionid);
        }

        [HttpPost]
        public IActionResult Delete(Permission Permission)
        {
            Permission Permissionid = PermissionService.FindPermissionById(Permission.PermissionId);

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            bool PermissionId = PermissionService.DeletePermission(Permissionid);

            TempData["Result"] = PermissionId ? "true" : "false";
            return RedirectToAction(nameof(Index));

        }
        #endregion

    }
}
