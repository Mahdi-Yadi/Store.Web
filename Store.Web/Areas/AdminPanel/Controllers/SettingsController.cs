using Application.Services.Site;
using DataLayer.Entities.Sites;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.AdminPanel.Controllers;
public class SettingsController : AdminBaseController
{

    private readonly ISettingService _settingService;

    public SettingsController(ISettingService settingService)
    {
        _settingService = settingService;
    }

    [HttpGet("SiteSetting")]
    public IActionResult SiteSetting()
    {
        var site = _settingService.GetSetting();

        if (site == null)
        {
            return NotFound();
        }

        return View(site);
    }

    [HttpPost("SiteSetting")]
    public IActionResult SiteSetting(Setting setting,IFormFile imageFile)
    {
        var res = _settingService.UpdateSetting(setting, imageFile);
        if (res)
        {
            TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
            return Redirect("/");
        }

        TempData[WarningMessage] = "عملیات شکست خورد.";
        return View(setting);
    }

}