using Microsoft.AspNetCore.Mvc;
using TaskTrackerAPI.DTOs;
using TaskTrackerAPI.Models;
using TaskTrackerAPI.Repositories;
using TaskTrackerAPI.Services;

namespace TaskTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository userRepository, IJwtTokenService jwtTokenService, ILogger<UserController> logger)
    : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly ILogger<UserController> _logger = logger;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        _logger.LogInformation("Registering {Username}", registerDto.Username);
        var existingUser = await _userRepository.GetUserByUsernameAsync(registerDto.Username);
        if (existingUser != null) return Conflict("Username is already taken.");

        var newUser = new UserModel
        {
            Username = registerDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };

        await _userRepository.AddUserAsync(newUser);
        return Ok(new { message = "UserModel registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        _logger.LogInformation("Logging user {Username}", loginDto.Username);
        var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            return Unauthorized("Invalid username or password");

        var token = _jwtTokenService.GenerateToken(user);
        return Ok(new { Token = token });
    }
}