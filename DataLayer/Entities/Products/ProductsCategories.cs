using DataLayer.Entities.Categories;
using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Products;
public class ProductsCategories
{
    [Key]
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public Category Category { get; set; }

}