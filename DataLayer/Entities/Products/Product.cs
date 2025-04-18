using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Discounts;
using DataLayer.Entities.Orders;

namespace DataLayer.Entities.Products;
public class Product 
{
    [Key]
    public int Id { get; set; }

    [MaxLength(500)]
    public string Title { get; set; }

    public string Description { get; set; }

    [MaxLength(50)]
    public string ImageName { get; set; }

    public bool IsSpecial { get; set; }

    public decimal Price { get; set; }

    public long TotalVisit { get; set; }

    public DateTime CreateDate { get; set; }

    public ICollection<ProductsCategories> ProductsCategories { get; set; }

    public ICollection<Discount> Discounts { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }

}