using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Sites;
public class Setting
{
    [Key]
    public int Id { get; set; }

    public string LogoImageName { get; set; }

    public string FooterText { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string AboutUs { get; set; }

}