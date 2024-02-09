using Application.Services.Account;
using Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Extentions;
namespace Store.Web.Areas.UserPanel.Controllers;
public class ProfileController : UserPanelBaseController
{

    private readonly IUserService _userService;
    public ProfileController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("EditProfile")]
    public IActionResult EditProfile()
    {
        var user = _userService.GetUserInfo(User.GetUserId());
        if(user == null)
        {
            return Redirect("/");
        }
        return View(user);
    }

    [HttpPost("EditProfile")]
	public IActionResult EditProfile(EditProdileDTO editProdile)
	{
        var res = _userService.EditUserProfile(editProdile);
        if(res)
        {
            ViewBag.isSuccess = true;
		}
		return View(editProdile);
	}

	[HttpGet("ChangePassword")]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost("ChangePassword")]
    public IActionResult ChangePassword(ChangePasswordDTO changePassword)
    {
        var res = _userService.ChangeUserPassword(changePassword,User.GetUserId());
        if(res)
        {
            ViewBag.success = true;
        }
        else
        {
            ViewBag.success = false;
        }
        return View(changePassword);
    }

}