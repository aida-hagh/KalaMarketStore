using KalaMarketStore.Core.Service.Province;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KalaMarketStore.DataLayer.Entities.Roles;
using KalaMarketStore.Core.Service.RolePermission;

namespace KalaMarketStore.Areas.Admin.Controllers;


[Area("Admin")]
public class RoleController : Controller
{
    private readonly IRoleService roleService;
    public RoleController(IRoleService roleService)
    {
        this.roleService = roleService;
    }



    #region Index
    public IActionResult Index()
    {
 
        return View(roleService.ShowAllRole());
    }
    #endregion


    #region Add
    [HttpGet]
    public IActionResult Add()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Add(Role Role)
    {
        if (!ModelState.IsValid)
        {

            return RedirectToAction(nameof(Index));
        }

        if (roleService.ExistRole(Role.RoleTitle, 0))
        {
            ModelState.AddModelError("CategoryEnTitle", "  تکراری است.");
            return View(Role);
        }

        int RoleId = roleService.AddRole(Role);

        TempData["Result"] = RoleId > 0 ? "true" : "false";
        return RedirectToAction(nameof(Index));


    }
    #endregion


    #region Edit
    [HttpGet]
    public IActionResult Edit(int id)
    {

        return View(roleService.FindRoleById(id));

    }


    [HttpPost]
    public IActionResult Edit(Role Role)
    {
        if (!ModelState.IsValid)
        {

            return RedirectToAction(nameof(Index));
        }

        if (roleService.ExistRole(Role.RoleTitle, Role.RoleId))
        {
            ModelState.AddModelError("CategoryEnTitle", "  تکراری است.");
            return View(Role);
        }
        bool RoleId = roleService.UpdateRole(Role);

        TempData["Result"] = RoleId ? "true" : "false";
        return RedirectToAction(nameof(Index));
    }
    #endregion


    #region Delete
    [HttpGet]
    public IActionResult Delete(int id)
    {
        Role Roleid = roleService.FindRoleById(id);
        if (Roleid == null)
        {
            return NotFound();
        }
        return View(Roleid);
    }

    [HttpPost]
    public IActionResult Delete(Role Role)
    {
        Role Roleid = roleService.FindRoleById(Role.RoleId);

        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        bool RoleId = roleService.DeleteRole(Roleid);

        TempData["Result"] = RoleId ? "true" : "false";
        return RedirectToAction(nameof(Index));

    }
    #endregion


}


