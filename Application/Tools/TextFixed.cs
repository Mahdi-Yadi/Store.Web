namespace Application.Tools;
public class TextFixed
{
	public static string FixEmail(string email)
	{
		return email.Trim().ToLower();
	}
}