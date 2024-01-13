using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class ProductController : Controller
{
	[HttpGet("ProductsList")]
	public IActionResult ProductsList()
    {
        return View();
    }

	[HttpGet("ProductDetail")]
	public IActionResult ProductDetail()
    {
        return View();
    }
}