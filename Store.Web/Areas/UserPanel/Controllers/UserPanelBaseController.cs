using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.UserPanel.Controllers;
[Authorize]
[Area("UserPanel")]
[Route("Panel")]
public class UserPanelBaseController : Controller
{
    protected string ErrorMessage = "ErrorMessage";
    protected string SuccessMessage = "SuccessMessage";
    protected string InfoMessage = "InfoMessage";
    protected string WarningMessage = "WarningMessage";
}