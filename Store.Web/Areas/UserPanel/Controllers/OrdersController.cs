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

    [HttpGet("Order/{id}")]
    public IActionResult Order(int id)
    {
        var o = _orderService.GetOrder(id);

        if (o != null)
        {
            if (o.UserId != User.GetUserId())
            {
                return Redirect("/");
            }
        }

        return View(o);
    }

    [HttpGet("DeleteOrder/{id}")]
    public IActionResult DeleteOrder(int orderId,int id)
    {
        var res = _orderService.DeleteOrder(id);

        if(res)
        {
            return Redirect($"/Panel/Order/{orderId}");
        }

        return Redirect($"/Panel/Order/{orderId}");
    }

    [HttpGet("OpenOrder")]
    public IActionResult OpenOrder()
    {
        return View();
    }

}