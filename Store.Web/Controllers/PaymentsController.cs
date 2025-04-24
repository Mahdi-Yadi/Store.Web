using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Controllers;
public class PaymentsController : Controller
{


    public IActionResult Pay()
    {
        return View();
    }

    public IActionResult VerfiyPay()
    {
        return View();
    }

}