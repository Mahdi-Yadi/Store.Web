using DataLayer.Entities.Account;
using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Orders;
public class Order
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Code { get; set; }

    public string CodePayment { get; set; }

    public decimal SumPrice { get; set; }

    public bool IsCompleted { get; set; }

    public string PostCode { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? CreateDatePayment { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }

    public User User { get; set; }

}