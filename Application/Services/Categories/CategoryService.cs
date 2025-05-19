using DataLayer.Contexts;
using DataLayer.Entities.Categories;
using Domain.Categories;
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

    public CreateCategoryResult CreateCategory(CreateCategoryDTO create)
    {
        if (create.Title == null)
            return CreateCategoryResult.Null;

        Category category = new Category();

        category.Name = create.Title;   
        category.ParentId = create.Id;

        _context.Categories.Add(category);
        _context.SaveChanges();
        return CreateCategoryResult.Success;

    }

    public EditCategoryDTO GetEditCat(int id)
    {
        var cat = _context.Categories.FirstOrDefault(c => c.Id == id);
        if(cat == null)
            return null;
        EditCategoryDTO edit = new EditCategoryDTO();
        edit.Id = cat.Id;
        edit.ParentId = cat.ParentId;
        edit.Title = cat.Name;
        return edit;    
    }

    public EditCategoryResult EditCategory(EditCategoryDTO edit)
    {
        var cat = _context.Categories.FirstOrDefault(c => c.Id == edit.Id);
        if (cat == null)
            return EditCategoryResult.Null;

        cat.Name = edit.Title;

        _context.SaveChanges();
        return EditCategoryResult.Success;
    }

    public bool DeleteCategory(int id)
    {
        var cat = _context.Categories.FirstOrDefault(c => c.Id == id);
        if(cat == null)
            return false;

        var parentCat = _context.Categories.Where(c => c.ParentId == cat.Id).ToList();

        if (parentCat.Any() && parentCat != null)
        {
            _context.Categories.RemoveRange(parentCat);
            _context.SaveChanges();
        }

        _context.Categories.Remove(cat);
        _context.SaveChanges();

        return true;
    }

    #endregion

}