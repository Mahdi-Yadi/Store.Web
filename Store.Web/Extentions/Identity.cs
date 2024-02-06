using System.Security.Claims;
namespace Store.Web.Extentions;
public static class Identity
{
	public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
	{
		if (claimsPrincipal != null)
		{
			var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
			if (data != null) return (int)Convert.ToInt64(data.Value);
		}
		return default(int);
	}
}