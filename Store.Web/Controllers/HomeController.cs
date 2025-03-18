using Application.Services.Products;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class HomeController : Controller
{

    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        ViewBag.special = _productService.GetSpecialProducts();

        return View();
    }
}