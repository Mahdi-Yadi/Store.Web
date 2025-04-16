using DataLayer.Entities.Orders;
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
    [MaxLength(500)]
    [MinLength(5)]
    [DataType(DataType.Text)]
    public string Address { get; set; }
	[MaxLength(100)]
	[MinLength(3)]
	[DataType(DataType.Text)]
	public string AddressCode { get; set; }
	[MaxLength(40)]
	[MinLength(3)]
	[DataType(DataType.PhoneNumber)]
	public string PhoneNumber { get; set; }
	public DateTime CreateDate { get; set; }
	public bool IsAdmin { get; set; }

    public ICollection<Order> Orders { get; set; }
}