using System.ComponentModel.DataAnnotations;
namespace Domain.Categories;
public class EditCategoryDTO
{
    public int Id { get; set; }
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "بیش از حد وارد کرده ای")]
    public string Title { get; set; }
    [Display(Name = "زیر گروه")]
    [MaxLength(3, ErrorMessage = "بیش از حد وارد کرده ای")]
    public int? ParentId { get; set; }
}
public enum EditCategoryResult
{
    Success,
    Null
}
