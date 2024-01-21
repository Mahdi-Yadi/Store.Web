using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.UserPanel.ViewComponents;
public class MenuViewComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		return View("Menu");
	}
}