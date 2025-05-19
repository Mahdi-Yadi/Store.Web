using Application.Services.Products;
using Domain.Products;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class HomeController : BaseController
{

    private readonly IProductService _productService;
   
    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        ViewBag.special = _productService.GetSpecialProducts();

        ViewBag.last = _productService.GetLastProducts();

        ViewBag.discount = _productService.GetProductsHasDiscount();

        ViewBag.popular = _productService.GetPopularProducts();

        return View();
    }
}