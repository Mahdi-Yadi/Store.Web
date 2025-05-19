using Application.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Extentions;

namespace Store.Web.Areas.UserPanel.Controllers;
public class HomeController : UserPanelBaseController
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("Home")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("FavProducts")]
    public IActionResult FavProducts()
    {
        var p = _productService.GetFavProducts(User.GetUserId());


        return View(p);
    }

    [HttpGet("DeleteFav/{id}")]
    public IActionResult DeleteFav(int id)
    {
        var p = _productService.DeleteProductToFav(id,User.GetUserId());
        TempData[ErrorMessage] = "محصول حذف شد.";
        return RedirectToAction(nameof(FavProducts));
    }

}