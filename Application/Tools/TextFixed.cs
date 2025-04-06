using System.Globalization;
namespace Application.Tools;
public static class TextFixed
{
	public static string FixEmail(string email)
	{
		return email.Trim().ToLower();
	}

    public static DateTime ToMiladi(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, new PersianCalendar());
    }

}