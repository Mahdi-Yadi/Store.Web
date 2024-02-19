using DataLayer.Contexts;
using DataLayer.Entities.Categories;
namespace Application.Services.Categories;
public class CategoryService : ICategoryService
{

    #region Constructor

    private readonly DBContext _context;

    public CategoryService(DBContext context)
    {
        _context = context;
    }

    #endregion

    #region Category

    public List<Category> GetAllCategory()
    {
        return _context.Categories.ToList();
    }

    #endregion

}