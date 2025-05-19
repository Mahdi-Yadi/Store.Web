using Application.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Extentions;
namespace Store.Web.Controllers;
[Authorize]
public class OrdersController : BaseController
{

    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    //[HttpPost("AddOrder/{productId}")]
    public IActionResult AddOrder(int productId)
    {
        int userId = User.GetUserId();

        var res = _orderService.AddOrder(productId, userId);

        if(res)
        {
            return Redirect($"/ProductDetail/{productId}?state=true");
        }
        else
        {
            return Redirect($"/ProductDetail/{productId}?state=true");
        }

    }


}