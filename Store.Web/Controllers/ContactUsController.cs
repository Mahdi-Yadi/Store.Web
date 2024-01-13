using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class ContactUsController : Controller
{
	[HttpGet("ContactUs")]
	public IActionResult ContactUs()
    {
        return View();
    }
}