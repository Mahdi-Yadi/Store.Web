using System.ComponentModel.DataAnnotations;
namespace Domain.Account;
public class EditProdileDTO
{
	public int Id { get; set; }
	[MaxLength(50)]
	[DataType(DataType.Text)]
	public string UserName { get; set; }
	[MaxLength(150)]
	[Required]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }
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
}