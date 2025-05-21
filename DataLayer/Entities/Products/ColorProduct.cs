using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Products;
public class ColorProduct
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; }

    public string ColorCode { get; set; }

    public int Count { get; set; }

    public float Price { get; set; }

    public bool IsDelete { get; set; }

    public long ProductId { get; set; }

    public Product Product { get; set; }

}