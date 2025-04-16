using DataLayer.Entities.Orders;

namespace Application.Services.Orders;
public interface IOrderService
{

    bool AddOrder(int productId,int userId);

    List<Order> OrdersList(int userId);

}