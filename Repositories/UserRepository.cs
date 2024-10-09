using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.Data;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<UserModel?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddUserAsync(UserModel userModel)
    {
        await context.Users.AddAsync(userModel);
        await context.SaveChangesAsync();
    }
}