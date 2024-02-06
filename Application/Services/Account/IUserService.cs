using DataLayer.Entities.Account;
using Domain.Account;
namespace Application.Services.Account;
public interface IUserService
{

    #region Account

    bool CreateUser(RegisterDTO register);

    bool IsEmailExist(string email);

    User LoginUser(LoginDTO login);

    EditProdileDTO GetUserInfo(int id);

    bool EditUserProfile(EditProdileDTO editProdile);

    #endregion

}