using DataLayer.Entities.Products;
using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Categories;
public class Category
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public int? ParentId { get; set; }

    public ICollection<ProductsCategories> ProductsCategories { get; set;}

    public Category Parent { get; set; }
}