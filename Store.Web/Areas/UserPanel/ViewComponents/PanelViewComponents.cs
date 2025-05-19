using Application.Services.Account;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.UserPanel.ViewComponents;
public class MenuViewComponent : ViewComponent
{
	private readonly IUserService _userService;

    public MenuViewComponent(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
	{
        ViewBag.user = _userService.CheckUser(User.Identity.Name);
		return View("Menu");
	}
}