using Application.Services.Products;
using Application.Services.Sliders;
using Domain.Products;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class HomeController : BaseController
{

    private readonly IProductService _productService;
    private readonly ISliderService _sliderService;

    public HomeController(IProductService productService, ISliderService sliderService)
    {
        _productService = productService;
        _sliderService = sliderService;
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
}