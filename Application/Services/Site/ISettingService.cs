using DataLayer.Contexts;
using DataLayer.Entities.Sites;
using Microsoft.AspNetCore.Http;
namespace Application.Services.Site;
public interface ISettingService
{

    bool UpdateSetting(Setting setting,IFormFile imageFile);

    Setting GetSetting();

}

public class SettingService : ISettingService
{
    private readonly DBContext _dbContext;

    public SettingService(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Setting GetSetting()
    {
        var setting = _dbContext
            .Settings
            .FirstOrDefault();

        if (setting == null)
        {
            Setting newSetting = new Setting();

            newSetting.FooterText = "متن تست";

            _dbContext.Settings.Add(newSetting);
            _dbContext.SaveChanges();

            return newSetting;
        }

        return setting;
    }

    public bool UpdateSetting(Setting setting, IFormFile imageFile)
    {
        try
        {
            var siteSetting = _dbContext
                .Settings
                .FirstOrDefault(a => a.Id == setting.Id);

            if (siteSetting == null)
                return false;

            siteSetting.FooterText = setting.FooterText;
            siteSetting.Address = setting.Address;
            siteSetting.Email = setting.Email;
            siteSetting.PhoneNumber = setting.PhoneNumber;
            siteSetting.AboutUs = setting.AboutUs;

            if (imageFile != null)
            {
                var imageName = Guid.NewGuid().ToString("N") + imageFile.FileName;
                string path = "wwwroot/images/Site/";
                var image = path + imageName;

                using (var stream = new FileStream(image, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                siteSetting.LogoImageName = imageName;
            }
            else
            {
                siteSetting.LogoImageName = "No-Image";
            }

            _dbContext.Settings.Update(siteSetting);
            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}