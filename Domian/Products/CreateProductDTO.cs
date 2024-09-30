using System.ComponentModel.DataAnnotations;
namespace Domain.Products;
public class CreateProductDTO
{
    [MaxLength(500)]
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

}