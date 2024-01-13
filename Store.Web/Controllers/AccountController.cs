using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class AccountController : Controller
{
    [HttpGet("Register")]
	public IActionResult Register()
	{
		return View();
	}

	[HttpGet("Login")]
	public IActionResult Login()
    {
        return View();
    }

	[HttpGet("Logout")]
	public IActionResult Logout()
    {
        return View();
    }

	[HttpGet("AccessDenied")]
	public IActionResult AccessDenied()
    {
        return View();
    }

}