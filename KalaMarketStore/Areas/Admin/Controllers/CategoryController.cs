using KalaMarketStore.Core.Service.Category;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace KalaMarketStore.Areas.Admin.Controllers;



[Area("Admin")]
public class CategoryController : Controller
{
    private ICategoryService categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }


    #region Index

    public IActionResult Index()
    {
        return View(categoryService.ShowMainCategory());
    }
    #endregion

    public IActionResult GetSubCategory(int cateid)
    {
        var cate = categoryService.ShowSubCategory(cateid);
        return new JsonResult(cate);

    }


    #region Add
    [HttpGet]
    public IActionResult Add(int? id)
    {
        ViewBag.Id = id;
        return View();
    }

    [HttpPost]
    public IActionResult Add(Category category, int? id)
    {
        if (!ModelState.IsValid)
            return View(category);
        category.SubCategiry = id;
        if (categoryService.ExistCategory(category.CategoryFaTitle, category.CategoryEnTitle, 0))
        {
            ModelState.AddModelError("CategoryEnTitle", "دسته بندی تکراری است.");
            return View(category);
        }

        int cateid = categoryService.AddCategory(category);

        TempData["Result"] = cateid > 0 ? "true" : "false";
        return RedirectToAction(nameof(Index));
    }

    #endregion



    #region IndexFirstSubcategory

    [HttpGet]
    public IActionResult IndexFirstSubcategory(int id)
    {
        ViewBag.Id = id;
        return View(categoryService.ShowSubCategory(id));
    }


 

    #endregion



    #region IndexSecondSubcategory
    [HttpGet]
    public IActionResult IndexSecondSubcategory(int id)
    {
        ViewBag.Id = id;
        return View(categoryService.ShowSubCategory(id));
    }
    #endregion




    #region Edit
    [HttpGet]
    public IActionResult Edit(int id)
    {
        ViewBag.Id = id;
        if (id == 0)
            return NotFound();

        var category = categoryService.FindCategoryById(id);

        if (category == null)
            return NotFound();

        return View(category);
    }


    [HttpPost]
    public IActionResult Edit(Category category)
    {

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        if (categoryService.ExistCategory(category.CategoryFaTitle, category.CategoryEnTitle, category.CategoryId))
        {
            ModelState.AddModelError("CategoryEnTitle", "دسته بندی تکراری است.");
            return View(category);
        }
        var _category = categoryService.FindCategoryById(category.CategoryId);
        category.SubCategiry = _category.SubCategiry;   
        var cate = categoryService.UpdateCategory(category);

        TempData["Result"] = cate ? "true" : "false";
        return RedirectToAction(nameof(Index));


    }
    #endregion




    #region Delete
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0)
            return NotFound();

        var category = categoryService.FindCategoryById(id);

        if (category == null)
        {

            TempData["NotFoundCategory"] = "true";
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Category category, int id)
    {
        try
        {
            if (id != 0)
            {
                var _category = categoryService.FindCategoryById(id);
                _category.IsDelete = true;
                categoryService.DeleteCategory(_category);
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    #endregion

}

