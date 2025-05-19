using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.AdminPanel.Controllers;
public class HomeController : AdminBaseController
{

    [HttpGet("Home")]
    public IActionResult Index()
    {
        return View();
    }
}