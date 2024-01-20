using Domain.Account;
namespace Application.Services.Account;
public interface IUserService
{

    #region Account

    bool CreateUser(RegisterDTO register);

    #endregion

}