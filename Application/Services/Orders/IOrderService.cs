using DataLayer.Entities.Orders;
namespace Application.Services.Orders;
public interface IOrderService
{

    bool AddOrder(int productId,int userId);

    List<Order> OrdersList(int userId);

    List<Order> OrdersListAdmin();

    Order GetOrder(int orderId);

    Order GetOpenOrder(int userId);

    bool CompleteOrderByAdmin(string code, string postCode);

    bool DeleteOrder(int orderDetailId);

    Task<bool> UpdateOrderForPay(long orderId, string trackingNumber);

    Task<bool> UpdateOrderAfterPayment(string transactionCode, string trackingNumber);

}