using Application.Services.Products;
using Application.Services.Site;
using Application.Services.Sliders;
using Domain.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace Store.Web.Controllers;
public class HomeController : BaseController
{

    private readonly IProductService _productService;
    private readonly ISliderService _sliderService;
    private readonly ISettingService _settingService;
    public HomeController(IProductService productService, ISliderService sliderService,
        ISettingService settingService)
    {
        _productService = productService;
        _sliderService = sliderService;
        _settingService = settingService;
    }

    public IActionResult Index()
    {
        ViewBag.special = _productService.GetSpecialProducts();

        ViewBag.last = _productService.GetLastProducts();

        ViewBag.discount = _productService.GetProductsHasDiscount();

        ViewBag.popular = _productService.GetPopularProducts();

        ViewBag.sliders = _sliderService.GetSlidersForSite(5);

        return View();
    }


    public IActionResult AboutUs()
    {
        ViewBag.site = _settingService.GetSetting();
        return View();
    }

}