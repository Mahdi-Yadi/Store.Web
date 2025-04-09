using DataLayer.Entities.Discounts;
namespace Domain.Products;
public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageName { get; set; }
    public bool IsSpecial { get; set; }
    public decimal Price { get; set; }
    public long TotalVisit { get; set; }
    public Discount Discount { get; set; }
}