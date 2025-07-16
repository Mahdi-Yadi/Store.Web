using DataLayer.Entities.Sites;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Sliders;
public interface ISliderService
{

    bool Add(Slider s,IFormFile imageFile);

    bool Delete(int id);

    List<Slider> GetSliders();

}