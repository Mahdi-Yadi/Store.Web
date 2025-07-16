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

    public IActionResult Sliders()
    {
        return View();
    }

    [HttpGet("Create-Slider")]
    public IActionResult CreateSlider()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
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


    [HttpGet("{id}")]
    public IActionResult DeleteSlider(int id)
    {
        return View();
    }

}