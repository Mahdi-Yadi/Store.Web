using Application.Services.Sliders;
using DataLayer.Entities.Sites;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.AdminPanel.Controllers;
[AutoValidateAntiforgeryToken]
public class SlidersController : AdminBaseController
{

    private readonly ISliderService _sliderService;

    public SlidersController(ISliderService sliderService)
    {
        _sliderService = sliderService;
    }

    [HttpGet("Sliders")]
    public IActionResult Sliders()
    {
        var sliders = _sliderService.GetSliders();
        return View(sliders);
    }

    [HttpGet("Create-Slider")]
    public IActionResult CreateSlider()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost("Create-Slider")]
    public IActionResult CreateSlider(Slider s,IFormFile imageFile)
    {
        if (ModelState.IsValid)
        {
            bool res = _sliderService.Add(s,imageFile);
            if (res)
            {
                TempData[SuccessMessage] = "اسلایدر ثبت شد";
                return RedirectToAction(nameof(Sliders));
            }

            TempData[WarningMessage] = "خطا در ثبت";
            return RedirectToAction(nameof(Sliders));
        }
        TempData[WarningMessage] = "فرم اسلایدر صحیح نیست!";
        return View(s);
    }


    [HttpGet("DeleteSlider/{id}")]
    public IActionResult DeleteSlider(int id)
    {
        var res = _sliderService.Delete(id);
        if (!res)
        {
            ViewBag.Mes = "مقدار را خالی وارد کرده اید";
            return RedirectToAction(nameof(Sliders));
        }
        else
        {
            ViewBag.Mes = "حذف با موفقیت انجام شد";
            return RedirectToAction(nameof(Sliders));
        }
    }

}