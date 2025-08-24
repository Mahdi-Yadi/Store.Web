using Application.Services.Products;
using Domain.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Extentions;
namespace Store.Web.Controllers;
public class ProductController : BaseController
{

    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("ProductsList")]
    public IActionResult ProductsList(string title = null,string category = null)
    {

        List<ProductDto> products = _productService.GetAllProducts(title,category);

        ViewBag.title = title; ViewBag.category = category;

        return View(products);
    }

	[HttpGet("ProductDetail/{id}")]
	public IActionResult ProductDetail(int id,string state = null)
    {
        ViewBag.state = state;

       var product = _productService.GetProductDetail(id);

        if (product == null)
            return Redirect("/");

        _productService.UpdateVisit(product.Id);

        return View(product);
    }

    [Authorize]
    [HttpGet("AddProductToFav/{id}")]
    public IActionResult AddProductToFav(int id)
    {
        var product = _productService.AddProductToFav(id,User.GetUserId());

        if (product == false)
        {
            TempData[WarningMessage] = "خطایی رخ داده!";
            return Redirect($"/ProductDetail/{id}");
        }
        
        TempData[SuccessMessage] = "محصول به موارد مورده علاقه اضافه شد.";
        return Redirect($"/ProductDetail/{id}");
    }

}