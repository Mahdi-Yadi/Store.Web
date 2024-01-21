using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.UserPanel.Controllers;
public class HomeController : UserPanelBaseController
{
    [HttpGet("Home")]
    public IActionResult Index()
    {
        return View();
    }
}