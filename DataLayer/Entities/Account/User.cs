using System.ComponentModel.DataAnnotations;
namespace DataLayer.Entities.Account;
public class User
{
	[Key]
	public int Id { get; set; }
	[MaxLength(50)]
	[DataType(DataType.Text)]
	public string UserName { get; set; }
    [MaxLength(150)]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [MaxLength(150)]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
	public DateTime CreateDate { get; set; }
}