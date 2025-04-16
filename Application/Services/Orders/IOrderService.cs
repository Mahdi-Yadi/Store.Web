namespace Application.Services.Orders;
public interface IOrderService
{

    bool AddOrder(int productId,int userId);

}