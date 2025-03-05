using Application.Services.Categories;
using Domain.Categories;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.AdminPanel.Controllers;
public class CategoryController : AdminBaseController
{

    #region Constructor

    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    #endregion

    #region Cat List

    [HttpGet("CategoriesList")]
    public IActionResult CategoriesList(string? mes)
    {
        ViewBag.Mes = mes;
        return View(_categoryService.GetAllCategory());
    }

    #endregion

    #region Create Category

    [HttpGet("CreateCategory/{id?}")]
    public IActionResult CreateCategory(int? id)
    {
        return View(new CreateCategoryDTO
        {
            Id = id
        });
    }

    [HttpPost("CreateCategory/{id?}")]
    public IActionResult CreateCategory(int? id, CreateCategoryDTO create)
    {
        if (ModelState.IsValid)
        {
            var res = _categoryService.CreateCategory(create);
            switch (res)
            {
                case CreateCategoryResult.Null:
                    ViewBag.Mes = " مقدار را خالی وارد کرده اید ";
                    break;
                case CreateCategoryResult.Success:
                    ViewBag.Mes = " دسته بندی با موفقیت ثبت شد ";
                    return RedirectToAction(nameof(CategoriesList));
            }
        }
        return View(create);
    }

    #endregion

    #region Edit Category

    [HttpGet("EditCategory/{id}")]
    public IActionResult EditCategory(int id)
    {
        var cat = _categoryService.GetEditCat(id);
        if (cat == null)
        {
            return RedirectToAction(nameof(CategoriesList));
        }

        return View(cat);
    }
    [HttpPost("EditCategory/{id}")]
    public IActionResult EditCategory(EditCategoryDTO edit)
    {
        if (ModelState.IsValid)
        {
            var res = _categoryService.EditCategory(edit);
            if (res == EditCategoryResult.Null)
            {
                ViewBag.Mes = "مقدار را خالی وارد کرده اید";
                return RedirectToAction(nameof(CategoriesList));
            }
            else
            {
                ViewBag.Mes = "ویرایش با موفقیت انجام شد";
                return RedirectToAction(nameof(CategoriesList));
            }
        }
        return View(edit);
    }

    #endregion

    #region Delete Category
    
    [HttpGet("DeleteCategory/{id}")]
    public ActionResult DeleteCategory(int id)
    {
        var res = _categoryService.DeleteCategory(id);
        if (!res)
        {
            ViewBag.Mes = "مقدار را خالی وارد کرده اید";
            return RedirectToAction(nameof(CategoriesList));
        }
        else
        {
            ViewBag.Mes = "حذف با موفقیت انجام شد";
            return RedirectToAction(nameof(CategoriesList));
        }
    }

    #endregion

}