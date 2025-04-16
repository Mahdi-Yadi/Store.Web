using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Orders;
public class OrderDetail
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int? DiscountId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    public Order Order{ get; set; }

}