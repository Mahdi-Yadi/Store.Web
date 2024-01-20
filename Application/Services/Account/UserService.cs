using Application.Tools;
using DataLayer.Contexts;
using DataLayer.Entities.Account;
using Domain.Account;
namespace Application.Services.Account;
public class UserService : IUserService
{
    #region Constructor

    private readonly DBContext _db;

    public UserService(DBContext dB)
    {
        _db = dB;
    }

    #endregion

    #region Account

    public bool CreateUser(RegisterDTO register)
    {
        var user = new User();

        user.Email = register.Email;
        user.CreateDate = DateTime.Now;
        user.Password = Hashing.EncodePasswordMd5(register.Password);

        _db.Add(user);
        _db.SaveChanges();

        return true;
    }

    #endregion

}