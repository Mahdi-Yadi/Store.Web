using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.UserPanel.Controllers;
public class OrdersController : UserPanelBaseController
{

    [HttpGet("OrdersList")]
    public IActionResult OrdersList()
    {
        return View();
    }

    [HttpGet("OpenOrder")]
    public IActionResult OpenOrder()
    {
        return View();
    }

}