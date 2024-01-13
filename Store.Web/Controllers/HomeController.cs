using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}