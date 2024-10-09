using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.Services;

public interface IJwtTokenService
{
    string GenerateToken(UserModel userModel);
}