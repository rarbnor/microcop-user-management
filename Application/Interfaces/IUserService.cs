using Application.Models;

namespace Application.Interfaces
{
	public interface IUserService
	{
        Task<UserModel> CreateUser(UserCreateModel userModel);

        Task<UserModel> GetUser(Guid id);

        Task<UserModel?> UpdateUser(Guid id, UserUpdateModel userUpdateModel);

        Task<UserModel?> DeleteUser(Guid id);

        Task<string?> GetUserApiKey(string apiKey);
    }
}

