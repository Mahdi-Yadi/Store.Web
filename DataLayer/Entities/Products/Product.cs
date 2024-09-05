using System.ComponentModel.DataAnnotations;
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

    public decimal Price { get; set; }

    public DateTime CreateDate { get; set; }

    public ICollection<ProductsCategories> ProductsCategories { get; set; }

}