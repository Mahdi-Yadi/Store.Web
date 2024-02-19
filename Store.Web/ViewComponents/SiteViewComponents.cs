using Application.Services.Categories;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.ViewComponents;
public class HeaderViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public HeaderViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService; 
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewBag.cat = _categoryService.GetAllCategory();
        return View("Header");
    }
}

public class FooterViewComponent : ViewComponent
{
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("Footer");
    }
}