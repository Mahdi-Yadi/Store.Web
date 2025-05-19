using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Products;
public class FavProduct
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
}