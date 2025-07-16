using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Sites;
public class Slider
{

    [Key]
    public int Id { get; set; }

    [DataType(DataType.Text)]
    [MaxLength(100)]
    public string Title { get; set; }

    public string ImageName { get; set; }

    public string ImageUrl { get; set; }

}