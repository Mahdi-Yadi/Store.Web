using Application.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Extentions;
namespace Store.Web.Areas.UserPanel.Controllers;
public class OrdersController : UserPanelBaseController
{
    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpGet("OrdersList")]
    public IActionResult OrdersList()
    {
        var o = _orderService.OrdersList(User.GetUserId());
        return View(o);
    }

    [HttpGet("OpenOrder")]
    public IActionResult OpenOrder()
    {
        return View();
    }

}