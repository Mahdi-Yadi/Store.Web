using Application.Services.Orders;
using DataLayer.Entities.Orders;
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
    public IActionResult OrdersList(int id = 0)
    {
        List<Order> o = new List<Order>();

        if (id == 0)
        {
            o = _orderService.OrdersListAdmin();
        }
        else
        {
            o = _orderService.OrdersList(id);
        }

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

    [HttpPost("CompleteOrder")]
    public IActionResult CompleteOrder(string code, string postcode)
    {
        if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(postcode))
        {
            var o = _orderService.CompleteOrderByAdmin(code, postcode);

            if (!o)
            {
                return Redirect("/");
            }
            return RedirectToAction(nameof(OrdersList));
        }
        return Redirect("/");
    }

}