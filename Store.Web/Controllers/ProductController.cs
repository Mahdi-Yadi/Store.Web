using Application.Services.Products;
using Domain.Products;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class ProductController : Controller
{

    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("ProductsList")]
	public IActionResult ProductsList()
    {

        List<ProductDto> products = _productService.GetProducts();

        return View(products);
    }

	[HttpGet("ProductDetail/{id}")]
	public IActionResult ProductDetail(int id)
    {
        var product = _productService.GetProductDetail(id);

        if (product == null)
            return Redirect("/");

        return View(product);
    }

}