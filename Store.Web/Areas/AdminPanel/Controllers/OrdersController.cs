using Application.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Extentions;
namespace Store.Web.Areas.AdminPanel.Controllers;
public class OrdersController : AdminBaseController
{

    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpGet("OrdersList")]
    public IActionResult OrdersList()
    {
        var o = _orderService.OrdersListAdmin();
        return View(o);
    }

    [HttpGet("Order/{id}")]
    public IActionResult Order(int id)
    {
        var o = _orderService.GetOrder(id);

        if (o == null)
        {
            if (o.UserId != User.GetUserId())
            {
                return Redirect("/");
            }
        }

        return View(o);
    }

}