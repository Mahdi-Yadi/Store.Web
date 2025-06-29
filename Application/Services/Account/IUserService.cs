using DataLayer.Entities.Account;
using Domain.Account;
using Domain.UsersAdmin;
namespace Application.Services.Account;
public interface IUserService
{

    #region Account

    bool CreateUser(RegisterDTO register);

    bool IsEmailExist(string email);

    User LoginUser(LoginDTO login);

    EditProdileDTO GetUserInfo(int id);

    bool EditUserProfile(EditProdileDTO editProdile);

    bool ChangeUserPassword(ChangePasswordDTO changePassword,int id);

    bool CheckUser(string email);

    #endregion

    #region Users

    List<UserDto> GetUsers();

    UserDto GetUser(int id);

    #endregion

}