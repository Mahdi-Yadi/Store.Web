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

}