using DataLayer.Entities.Products;
using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Orders;
public class OrderDetail
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int? DiscountId { get; set; }

    public int ProductId { get; set; }

    public int? ColorId { get; set; }

    public decimal ColorPrice { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }

    public Order Order{ get; set; }

    public Product Product { get; set; }

}