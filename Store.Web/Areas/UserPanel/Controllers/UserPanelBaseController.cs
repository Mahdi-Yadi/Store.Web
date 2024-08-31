using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.UserPanel.Controllers;
[Authorize]
[Area("UserPanel")]
//[Route("Panel")]
public class UserPanelBaseController : Controller
{
    
}