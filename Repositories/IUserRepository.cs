using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.Repositories;

public interface IUserRepository
{
    Task<UserModel?> GetUserByUsernameAsync(string username);
    Task AddUserAsync(UserModel userModel);
}