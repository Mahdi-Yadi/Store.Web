using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Products;
namespace DataLayer.Entities.Discounts;
public class Discount
{
    [Key]
    public int Id { get; set; }

    public int DiscountPercentage { get; set; }

    public DateTime ExpireDate { get; set; }

    public bool IsDeleted { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

}