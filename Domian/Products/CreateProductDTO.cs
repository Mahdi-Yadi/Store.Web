using System.ComponentModel.DataAnnotations;
namespace Domain.Products;
public class CreateProductDTO
{
    [MaxLength(500)]
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsSpecial { get; set; }

    public decimal Price { get; set; }

    public List<int> catsid { get; set; }

}