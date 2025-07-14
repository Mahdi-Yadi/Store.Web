using Application.Tools;
using DataLayer.Contexts;
using DataLayer.Entities.Account;
using Domain.Account;
using Domain.UsersAdmin;
using Microsoft.Win32;
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

		user.Email = TextFixed.FixEmail(register.Email);
		user.CreateDate = DateTime.Now;
		user.Password = Hashing.EncodePasswordMd5(register.Password);

		_db.Add(user);
		_db.SaveChanges();

		return true;
	}

	public bool IsEmailExist(string email)
	{
		return _db.Users.Any(u => u.Email == email);
	}

	public User LoginUser(LoginDTO login)
	{
		string email = TextFixed.FixEmail(login.Email);
		string pass = Hashing.EncodePasswordMd5(login.Password);
		return _db.Users.SingleOrDefault(u => u.Email == email && u.Password == pass);
	}

	public EditProdileDTO GetUserInfo(int id)
	{
		var user = _db.Users.SingleOrDefault(u => u.Id == id);
		EditProdileDTO editProdile = new EditProdileDTO();
		editProdile.Id = user.Id;
		editProdile.UserName = user.UserName;
		editProdile.Email = user.Email;
		editProdile.Address = user.Address;
		editProdile.AddressCode = user.AddressCode;
		editProdile.PhoneNumber = user.PhoneNumber;
		return editProdile;
	}

	public bool EditUserProfile(EditProdileDTO editProdile)
	{
		var user = _db.Users.FirstOrDefault(u => u.Id == editProdile.Id);
		if (user == null)
			return false;

		user.UserName = editProdile.UserName;
		user.Email = TextFixed.FixEmail(editProdile.Email);
		user.Address = editProdile.Address;
		user.AddressCode = editProdile.AddressCode;
		user.PhoneNumber = editProdile.PhoneNumber;

		_db.Update(user);
		_db.SaveChanges();

		return true;
	}

	public bool ChangeUserPassword(ChangePasswordDTO changePassword, int id)
	{
		var user = _db.Users.FirstOrDefault(u => u.Id == id);
		if (user == null)
			return false;
		var oldPassword = Hashing.EncodePasswordMd5(changePassword.OldPassword);
		if (oldPassword != user.Password)
			return false;
		user.Password = Hashing.EncodePasswordMd5(changePassword.NewPassword);
		_db.Users.Update(user);
		_db.SaveChanges();
		return true;
	}

	public bool CheckUser(string email)
	{
		return _db.Users.Any(u => u.Email == email && u.IsAdmin);
	}

    #endregion

    #region Users

    public List<UserDto> GetUsers()
    {
        var users = _db.Users
            .ToList();

        if (users.Count == 0)
            return new List<UserDto>();

        List<UserDto> dtos = new List<UserDto>();

        foreach (var item in users)
        {
            var a = new UserDto()
            {
				Id= item.Id,
				Email = item.Email,
				FullName = item.UserName,
				PhoneNumber = item.PhoneNumber,
				IsAdmin = item.IsAdmin,
                IsDelete = item.IsDelete,
            };
			dtos.Add(a);
        }

        return dtos;
    }

    public UserDto GetUser(int id)
    {
		var u = _db.Users.FirstOrDefault(u => u.Id == id);

		if (u == null) return null;

        UserDto dto = new UserDto();

		dto.Id = id;
		dto.Email = u.Email;
		dto.PhoneNumber = u.PhoneNumber;
        dto.FullName = u.UserName;
		dto.Address = u.Address;
		dto.AddressCode = u.AddressCode;

        return dto;
    }

    public bool DeleteUser(int id)
    {
        var u = _db.Users.FirstOrDefault(u => u.Id == id);

        if (u == null) return false;

		u.IsDelete = true;

        _db.Users.Update(u);
        _db.SaveChanges();

		return true;
    }

    public bool RecoverUser(int id)
    {
        var u = _db.Users.FirstOrDefault(u => u.Id == id);

        if (u == null) return false;

        u.IsDelete = false;

        _db.Users.Update(u);
        _db.SaveChanges();

        return true;
    }

    public bool SetUserToAdmin(int id)
    {
        var u = _db.Users.FirstOrDefault(u => u.Id == id);

        if (u == null) return false;

        u.IsAdmin = true;

        _db.Users.Update(u);
        _db.SaveChanges();

        return true;
    }

    public bool GetAdminFromUser(int id)
    {
        var u = _db.Users.FirstOrDefault(u => u.Id == id);

        if (u == null) return false;

        u.IsAdmin = false;

        _db.Users.Update(u);
        _db.SaveChanges();

        return true;
    }

    #endregion

}