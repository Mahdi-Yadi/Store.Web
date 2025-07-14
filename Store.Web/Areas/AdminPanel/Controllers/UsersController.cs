using Application.Services.Account;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.AdminPanel.Controllers;
public class UsersController : AdminBaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("UsersList")]
    public IActionResult UsersList()
    {
        var users = _userService.GetUsers();


        return View(users);
    }


    [HttpGet("UserInfo/{id}")]
    public IActionResult UserInfo(int id)
    {
        var user = _userService.GetUser(id);

        if (user == null)
            return Redirect("/");

        return View(user);
    }

    [HttpGet("DeleteUser/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var res = _userService.DeleteUser(id);

        if (res == true)
        {
            TempData[SuccessMessage] = "کاربر با موفقیت حذف شد";
            return RedirectToAction(nameof(UsersList));
        }
        TempData[WarningMessage] = "کاربر یافت نشد";
        return RedirectToAction(nameof(UsersList));
    }

    [HttpGet("RecoverUser/{id}")]
    public IActionResult RecoverUser(int id)
    {
        var res = _userService.RecoverUser(id);

        if (res == true)
        {
            TempData[SuccessMessage] = "کاربر با موفقیت بازگردانده شد";
            return RedirectToAction(nameof(UsersList));
        }
        TempData[WarningMessage] = "کاربر یافت نشد";
        return RedirectToAction(nameof(UsersList));
    }

    [HttpGet("SetUserToAdmin/{id}")]
    public IActionResult SetUserToAdmin(int id)
    {
        var result = _userService.SetUserToAdmin(id);

        if (result == false)
        {
            TempData[WarningMessage] = "عملیات با شکست مواجه شد.";
            return RedirectToAction(nameof(UsersList));
        }

        TempData[SuccessMessage] = "کاربر به ادمین تغییر پیدا کرد.";
        return RedirectToAction(nameof(UsersList));
    }


    [HttpGet("GetAdminFromUser/{id}")]
    public IActionResult GetAdminFromUser(int id)
    {
        var result = _userService.GetAdminFromUser(id);

        if (result == false)
        {
            TempData[WarningMessage] = "عملیات با شکست مواجه شد.";
            return RedirectToAction(nameof(UsersList));
        }

        TempData[SuccessMessage] = "کاربر به حالت عادی تغییر پیدا کرد.";
        return RedirectToAction(nameof(UsersList));
    }

}