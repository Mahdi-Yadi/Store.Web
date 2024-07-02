using DataLayer.Entities.Categories;
using Domain.Categories;
namespace Application.Services.Categories;
public interface ICategoryService
{
    List<Category> GetAllCategory();

    CreateCategoryResult CreateCategory(CreateCategoryDTO create);

    EditCategoryDTO GetEditCat(int id);

    EditCategoryResult EditCategory(EditCategoryDTO edit);

    bool DeleteCategory(int id);

}