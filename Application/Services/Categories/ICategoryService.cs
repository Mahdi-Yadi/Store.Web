using DataLayer.Entities.Categories;

namespace Application.Services.Categories;
public interface ICategoryService
{
    List<Category> GetAllCategory();
}