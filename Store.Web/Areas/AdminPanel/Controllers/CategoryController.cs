using Application.Services.Categories;
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
    public IActionResult CategoriesList()
    {
        return View(_categoryService.GetAllCategory());
    }

    #endregion

    #region Create Category

    [HttpGet("CreateCategory")]
    public IActionResult CreateCategory()
    {
        return View();
    }

    #endregion

    #region Edit Category

    [HttpGet("EditCategory")]
    public IActionResult EditCategory()
    {
        return View();
    }

    #endregion

}