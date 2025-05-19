using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class ContactUsController : BaseController
{
	[HttpGet("ContactUs")]
	public IActionResult ContactUs()
    {
        return View();
    }
}