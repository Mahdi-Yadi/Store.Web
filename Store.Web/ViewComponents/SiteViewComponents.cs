using System.Security.Claims;
using Application.Services.Categories;
using Application.Services.Orders;
using Application.Services.Site;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Extentions;
namespace Store.Web.ViewComponents;
public class HeaderViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;
    private readonly IOrderService _orderService;
    private readonly ISettingService _settingService;

    public HeaderViewComponent(ICategoryService categoryService, IOrderService orderService, ISettingService settingService)
    {
        _categoryService = categoryService;
        _orderService = orderService;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewBag.cat = _categoryService.GetAllCategory();
        ViewBag.openOrder = _orderService.GetOpenOrder(((ClaimsPrincipal)User).GetUserId());
        ViewBag.site = _settingService.GetSetting();
        return View("Header");
    }
}

public class FooterViewComponent : ViewComponent
{
    private readonly ISettingService _settingService;

    public FooterViewComponent(ISettingService settingService)
    {
        _settingService = settingService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewBag.site = _settingService.GetSetting();
        return View("Footer");
    }
}