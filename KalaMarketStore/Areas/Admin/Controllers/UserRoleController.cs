using KalaMarketStore.Core.Service.RolePermission;
using KalaMarketStore.Core.Service.User;
using KalaMarketStore.Core.Service.UserRole;
using KalaMarketStore.DataLayer.Entities.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace KalaMarketStore.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class UserRoleController : Controller
{
    private readonly IUserRoleService userRoleService;
    private readonly IUserService userService;
    private readonly IRoleService roleService;
    public UserRoleController(IUserRoleService userRoleService,
        IUserService userService, IRoleService roleService)
    {
        this.userRoleService = userRoleService;
        this.userService = userService;
        this.roleService = roleService;
    }


    #region Index
    public IActionResult Index()
    {
        return View(userRoleService.ShowUserRole());
    }

    #endregion


    #region Add
    [HttpGet]
    public IActionResult Add(int userid)
    {
        //int userId = int.Parse(User.FindFirst("UserId").Value);
        //userid = userId;
        //if (userid != 0)
        //{
        ViewBag.User = new SelectList(userService.ShowAllUser(), "UserId", "UserAccount");
        ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
        return View();

        //}
        //ViewBag.User = new SelectList(userService.ShowAllUser(), "UserId", "UserAccount");
        //ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");

        //return View();
    }

    [HttpPost]
    public IActionResult Add(UserRole userRole)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (userRoleService.ExistUserRole(userRole.UserId, userRole.RoleId, 0))
                {
                    ModelState.AddModelError("UserId", "  این نقش برای این کاربر قبلا انتخاب شده است.");
                    ViewBag.User = new SelectList(userService.ShowAllUser(), "UserId", "UserAccount");
                    ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
                    return View(userRole);
                }
                var URid = userRoleService.AddUserRole(userRole);
                TempData["Result"] = URid > 0 ? "true" : "false";

                return Redirect("/Admin/UserRole/Index");
            }
            //ViewBag.User = new SelectList(userService.FindUserByIdToList(userRole.UserId), "UserId", "UserName", "UserFamily");
            ViewBag.User = new SelectList(userService.ShowAllUser(), "UserId", "UserAccount");
            ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
            return View(userRole);
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
        ViewBag.User = new SelectList(userService.ShowAllUser(), "UserId", "UserAccount");
        ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
        var ur = userRoleService.FindUserRoleById(id);
        return View(ur);


    }


    [HttpPost]
    public IActionResult Edit(UserRole userRole)
    {
        if (ModelState.IsValid)
        {
            //var ur = userRoleService.FindUserRoleById(userRole.UserRoleId);

            if (userRoleService.ExistUserRole(userRole.UserId, userRole.RoleId, userRole.UserRoleId))
            {
                ModelState.AddModelError("RoleId", "  این نقش برای این کاربر قبلا انتخاب شده است.");
                ViewBag.User = new SelectList(userService.ShowAllUser(), "UserId", "UserAccount");
                ViewBag.Role = new SelectList(roleService.ShowAllRole(), "RoleId", "RoleTitle");
                return View(userRole);
            }
            var URid = userRoleService.UpdateUserRole(userRole);
            TempData["Result"] = URid ? "true" : "false";

            return Redirect("/Admin/UserRole/Index");
        }
        return View(userRole);
    }



    #endregion


    #region Delete
    [HttpGet]
    public IActionResult Delete(int id)
    {
        if (id == 0)
            return NotFound();

        var urid = userRoleService.FindUserRoleById(id);

        if (urid == null)
            return NotFound();

        return View(urid);
    }

    [HttpPost]
    public IActionResult Delete(UserRole userRole)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

     var userRoleid=  userRoleService.DeleteUserRole(userRole);
        TempData["Result"] = userRoleid ? "true" : "false";

        return Redirect("/Admin/UserRole/Index");
     
    }
    #endregion


}

