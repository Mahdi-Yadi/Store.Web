using DataLayer.Contexts;
using DataLayer.Entities.Account;
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

    public Order GetOrder(int oredrId)
    {
        var order = _db
            .Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.Product)
            .ThenInclude(o => o.Discounts)
            .Include(o => o.User)
            .FirstOrDefault(o => o.Id == oredrId);

        if (order == null)
            return new Order();

        return order;
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

}