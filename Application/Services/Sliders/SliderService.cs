using DataLayer.Contexts;
using DataLayer.Entities.Products;
using DataLayer.Entities.Sites;
using Microsoft.AspNetCore.Http;
namespace Application.Services.Sliders;
public class SliderService : ISliderService
{

    private readonly DBContext _db;

    public SliderService(DBContext db)
    {
        _db = db;
    }

    public bool Add(Slider s, IFormFile imageFile)
    {
        try
        {
            Slider slider = new Slider();

            if (imageFile != null)
            {
                var imageName = Guid.NewGuid().ToString("N") + imageFile.FileName;
                string path = "wwwroot/images/sliders/";
                var image = path + imageName;

                using (var stream = new FileStream(image, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                slider.ImageName = imageName;
            }

            slider.ImageUrl = s.ImageUrl;
            slider.Title = s.Title;

            _db.Sliders.Add(slider);
            _db.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var s = _db.Sliders.FirstOrDefault(s => s.Id == id);

            if (s == null)
                return false;

            if (s.ImageName != null)
            {
                var pathOld = Path.Combine("wwwroot/images/sliders/" + s.ImageName);
                if (File.Exists(pathOld))
                {
                    File.Delete(pathOld);
                }
            }

            _db.Sliders.Remove(s);
            _db.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<Slider> GetSliders()
    {
        return _db.Sliders.ToList();
    }

    public List<Slider> GetSlidersForSite(int take)
    {
        return _db.Sliders.Take(take).ToList();
    }

}