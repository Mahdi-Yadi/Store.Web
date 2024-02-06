using System.ComponentModel.DataAnnotations;
namespace Domain.Account;
public class ChangePasswordDTO
{
	[MaxLength(150)]
	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; }
	[MaxLength(150)]
	[Required]
	[DataType(DataType.Password)]
	[Compare("Password",ErrorMessage = "کلمات عبور جدید مطابقت ندارد")]
	public string RePassword { get; set; }
}