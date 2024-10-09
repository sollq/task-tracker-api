using Microsoft.AspNetCore.Mvc;
using TaskTrackerAPI.DTOs;
using TaskTrackerAPI.Models;
using TaskTrackerAPI.Repositories;
using TaskTrackerAPI.Services;

namespace TaskTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository userRepository, IJwtTokenService jwtTokenService)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var existingUser = await userRepository.GetUserByUsernameAsync(registerDto.Username);
        if (existingUser != null) return Conflict("Username is already taken.");

        var newUser = new UserModel
        {
            Username = registerDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };

        await userRepository.AddUserAsync(newUser);
        return Ok(new { message = "UserModel registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await userRepository.GetUserByUsernameAsync(loginDto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            return Unauthorized("Invalid username or password");

        var token = jwtTokenService.GenerateToken(user);
        return Ok(new { Token = token });
    }
}