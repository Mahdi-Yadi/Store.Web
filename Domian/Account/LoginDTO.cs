using System.ComponentModel.DataAnnotations;
namespace Domain.Account;
public class LoginDTO
{
	[MaxLength(150, ErrorMessage = "نمی توانید بیشتر از 150 کاراکتر وارد نمایید")]
	[Required(ErrorMessage = "ایمیل اجباری است")]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }
	[MaxLength(150, ErrorMessage = "نمی توانید بیشتر از 150 کاراکتر وارد نمایید")]
	[Required(ErrorMessage = "کلمه عبور اجباری است")]
	[DataType(DataType.Password)]
	public string Password { get; set; }
	public bool RemmberMe { get; set; }
}