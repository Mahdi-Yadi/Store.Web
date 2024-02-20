using DataLayer.Entities.Categories;
namespace Domain.Categories;
public class CategoryDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ParentId { get; set; }
    public Category Parent { get; set; }
}