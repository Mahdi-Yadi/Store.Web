using DataLayer.Entities.Account;
using Domain.Account;
namespace Application.Services.Account;
public interface IUserService
{

    #region Account

    bool CreateUser(RegisterDTO register);

    bool IsEmailExist(string email);

    User LoginUser(LoginDTO login);

    #endregion

}