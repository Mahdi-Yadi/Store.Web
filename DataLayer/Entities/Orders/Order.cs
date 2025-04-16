using DataLayer.Entities.Account;

namespace DataLayer.Entities.Orders;
public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Code { get; set; }

    public string CodePayment { get; set; }

    public decimal SumPrice { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime CreateDatePayment { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }

    public User User { get; set; }

}