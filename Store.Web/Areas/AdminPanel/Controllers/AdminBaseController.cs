using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
[Authorize]
[Route("PanelAdmin")]
public class AdminBaseController : Controller
{
   
}