using System.Security.Claims;
using Application.Services.Categories;
using Application.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Extentions;

namespace Store.Web.ViewComponents;
public class HeaderViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;
    private readonly IOrderService _orderService;

    public HeaderViewComponent(ICategoryService categoryService, IOrderService orderService)
    {
        _categoryService = categoryService;
        _orderService = orderService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewBag.cat = _categoryService.GetAllCategory();
        ViewBag.openOrder = _orderService.GetOpenOrder(((ClaimsPrincipal)User).GetUserId());
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