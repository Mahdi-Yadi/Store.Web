using Application.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parbad;
using Parbad.AspNetCore;
using Parbad.Gateway.ZarinPal;
using Store.Web.Extentions;
namespace Store.Web.Controllers;
public class PaymentsController : Controller
{

    private readonly IOrderService _orderService;
    private readonly IOnlinePayment _onlinePayment;

    public PaymentsController(IOrderService orderService, IOnlinePayment onlinePayment)
    {
        _orderService = orderService;
        _onlinePayment = onlinePayment;
    }

    [Authorize]
    [HttpGet("Pay")]
    public async Task<IActionResult> Pay()
    {
        var order = _orderService.GetOpenOrder(User.GetUserId());

        if (order == null || order.OrderDetails.Count == 0)
            return Redirect("/");

        decimal totalPrice = 0;

        foreach (var item in order.OrderDetails)
        {
            decimal price = item.Product.Price;

            var activeDiscounts = item.Product.Discounts
                ?.FirstOrDefault(d => d.IsDeleted == false && d.ExpireDate > DateTime.Now);

            if (activeDiscounts != null)
            {
                price -= price * (activeDiscounts.DiscountPercentage / 100m);
            }

            totalPrice += price;
        }

        try
        {
            string trakingNumber = $"{DateTime.Now:yyyyMMddHHmmmmssffff}";

            string callback = "https://localhost:44383/VerfiyPay";

            var res = await _onlinePayment.RequestAsync(invoice =>
            {
                invoice.SetZarinPalData("خرید از وب سایت Technoto")
                    .SetTrackingNumber(Convert.ToInt64(trakingNumber))
                    .SetAmount(totalPrice)
                    .SetCallbackUrl(callback)
                    .SetGateway("ZarinPal");
            });

            var resOrder = await _orderService.UpdateOrderForPay(order.Id, trakingNumber);
            if (!resOrder)
            {
                resOrder = await _orderService.UpdateOrderForPay(order.Id, trakingNumber);
                if (!resOrder)
                    return BadRequest();
            }

            if (res.IsSucceed)
                return res.GatewayTransporter.TransportToGateway();

        }
        catch (Exception)
        {
            return Redirect("/");
        }

        return Redirect("/");
    }

    [HttpGet("VerfiyPay")]
    [HttpPost("VerfiyPay")]
    public IActionResult VerfiyPay()
    {
        return View();
    }

}