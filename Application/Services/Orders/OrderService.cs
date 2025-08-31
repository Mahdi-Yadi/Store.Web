using Application.Services.Emails;
using DataLayer.Contexts;
using DataLayer.Entities.Orders;
using Microsoft.EntityFrameworkCore;
namespace Application.Services.Orders;
public class OrderService : IOrderService
{
    private readonly DBContext _db;

    public OrderService(DBContext db)
    {
        _db = db;
    }

    public bool AddOrder(int productId, int userId)
    {
        var oldOrder = _db.Orders.FirstOrDefault(a => a.UserId == userId &&
        a.CreateDatePayment == null);

        if (oldOrder != null)
        {
            var oldOD = _db.OrderDetails.FirstOrDefault(od => od.OrderId == oldOrder.Id &&
            od.ProductId == productId);

            if (oldOD != null)
            {
                oldOD.Count += 1;

                _db.OrderDetails.Update(oldOD);
            }
            else
            {
                OrderDetail od = new OrderDetail();

                od.ProductId = productId;
                od.OrderId = oldOrder.Id;
                od.Count = 1;

                _db.OrderDetails.Add(od);
            }

            _db.SaveChanges();

            return true;
        }
        else
        {
            Order o = new Order();

            DateTime dateTime = DateTime.Now;

            o.SumPrice = 0;
            o.CreateDate = dateTime;
            o.Code = dateTime.ToString("yyyyMMddHHmmss");
            o.UserId = userId;

            _db.Orders.Add(o);
            _db.SaveChanges();

            OrderDetail od = new OrderDetail();

            od.ProductId = productId;
            od.OrderId = o.Id;
            od.Count = 1;

            _db.OrderDetails.Add(od);
            _db.SaveChanges();

            return true;
        }

    }

    public List<Order> OrdersList(int userId)
    {
        var orders = _db
            .Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.Product)
            .ThenInclude(o => o.Discounts)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreateDate)
            .ToList();

        return orders;
    }

    public List<Order> OrdersListAdmin()
    {
        var orders = _db
            .Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.Product)
            .ThenInclude(o => o.Discounts)
            .OrderByDescending(o => o.CreateDate)
            .ToList();

        return orders;
    }

    public Order GetOrder(int orderId)
    {
        var order = _db
            .Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.Product)
            .ThenInclude(o => o.Discounts)
            .Include(o => o.User)
            .FirstOrDefault(o => o.Id == orderId);

        if (order == null)
            return new Order();

        return order;
    }

    public Order GetOpenOrder(int userId)
    {
        var order = _db
            .Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.Product)
            .ThenInclude(o => o.Discounts)
            .Include(o => o.User)
            .FirstOrDefault(o => o.CreateDatePayment == null && o.UserId == userId);

        if (order == null)
            return new Order();

        return order;
    }

    public bool CompleteOrderByAdmin(string code, string postCode)
    {
        var order = _db
            .Orders
            .FirstOrDefault(o => o.Code == code);

        if (order == null)
            return false;

        order.PostCode = postCode;
        order.IsCompleted = true;

        _db.Orders.Update(order);
        _db.SaveChanges();

        return true;
    }

    public bool DeleteOrder(int orderDetailId)
    {
        var order = _db
            .OrderDetails
            .FirstOrDefault(o => o.Id == orderDetailId);

        if (order == null)
            return false;

        _db.OrderDetails.Remove(order);
        _db.SaveChanges();
        return true;
    }

    public async Task<bool> UpdateOrderForPay(long orderId, string trackingNumber)
    {
        var o = await _db.Orders
            .FirstOrDefaultAsync(a => a.Id == orderId);

        if (o == null) return false;

        o.Code = trackingNumber;

        _db.Orders.Update(o);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateOrderAfterPayment(string transactionCode, string trackingNumber)
    {
        var o = await _db.Orders
            .Include(a => a.OrderDetails)
            .ThenInclude(a => a.Product)
            .ThenInclude(a => a.Discounts)
            .FirstOrDefaultAsync(a => a.Code == trackingNumber);

        if (o == null) return false;

        decimal totalPrice = 0;

        foreach (var item in o.OrderDetails)
        {
            decimal price = item.Product.Price;

            var activeDiscounts = item.Product.Discounts
                ?.FirstOrDefault(d => d.IsDeleted == false && d.ExpireDate > DateTime.Now);

            if (activeDiscounts != null)
            {
                if (activeDiscounts.IsForShow)
                {
                    price -= price * (activeDiscounts.DiscountPercentage / 100m);
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.DiscountCode) &&
                        !string.IsNullOrEmpty(activeDiscounts.Code) &&
                        item.DiscountCode == activeDiscounts.Code)
                    {
                        price -= price * (activeDiscounts.DiscountPercentage / 100m);
                    }
                }
            }

            totalPrice += price;
        }

        o.SumPrice = totalPrice;
        o.CodePayment = transactionCode;
        o.CreateDatePayment = DateTime.Now;

        _db.Orders.Update(o);
        await _db.SaveChangesAsync();

        SendEmailToAdmins(o.Code, o.CodePayment);

        return true;
    }

    private void SendEmailToAdmins(string sitecode, string paymentCode)
    {
        var users = _db.Users
            .Where(a => a.IsAdmin)
            .ToList();

        if (users.Count > 0)
        {
            string subject = "فاکتوری در وبسایت پرداخت گردید";
            string body = $"فاکتوری با شماره {sitecode} و با کد پیگیری {paymentCode} پرداخت گردید.";
            foreach (var user in users)
            {
                // Send Email
                EmailSender.SendEmail(user.Email, subject, body);
            }
        }
    }

    public bool SetDiscountCodeForOrderDeatil(int userId, string code)
    {
        var dis = _db.Discounts.FirstOrDefault(a => 
        a.IsDeleted == false
        && a.ExpireDate > DateTime.Now 
        && a.Code == code);

        if (dis == null)
            return false;

        var order = _db.Orders.FirstOrDefault(a=>a.UserId == userId);

        if(order == null) return false;

        var od = _db.OrderDetails
            .FirstOrDefault(a =>
            a.OrderId == order.Id 
            &&
            a.ProductId == dis.ProductId);

        if (od == null)
            return false;

        od.DiscountCode = code;

        _db.OrderDetails.Update(od);
        _db.SaveChanges();

        return true;
    }

}