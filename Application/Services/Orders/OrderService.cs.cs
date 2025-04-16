using DataLayer.Contexts;
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
        a.SumPrice != 0);

        if (oldOrder != null)
        {
            var oldOD = _db.OrderDetails.FirstOrDefault(od => od.OrderId == oldOrder.Id &&
            od.ProductId == productId);

            if (oldOD != null)
            {

            }
            else
            {

            }

            return true;
        }
        else
        {

            return true;
        }

    }

}