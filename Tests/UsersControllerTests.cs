using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TaskTrackerAPI.Controllers;
using TaskTrackerAPI.DTOs;
using TaskTrackerAPI.Models;
using TaskTrackerAPI.Repositories;
using TaskTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Legacy;

namespace TaskTrackerAPI.Tests
{
    public class UserControllerTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IJwtTokenService> _mockJwtTokenService;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJwtTokenService = new Mock<IJwtTokenService>();
            _controller = new UserController(_mockUserRepository.Object, _mockJwtTokenService.Object);
        }

        [Test]
        public async Task Register_ReturnsOk_WhenUserIsRegistered()
        {
            var registerDto = new RegisterDto { Username = "newuser", Password = "password" };
            _mockUserRepository
                .Setup(repo => repo.GetUserByUsernameAsync(registerDto.Username))
                .ReturnsAsync((UserModel)null);

            var result = await _controller.Register(registerDto);

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Register_ReturnsConflict_WhenUsernameAlreadyExists()
        {
            var registerDto = new RegisterDto { Username = "existinguser", Password = "password" };
            _mockUserRepository
                .Setup(repo => repo.GetUserByUsernameAsync(registerDto.Username))
                .ReturnsAsync(new UserModel
                {
                    Username = registerDto.Username,
                    Password = null
                });

            var result = await _controller.Register(registerDto);

            ClassicAssert.IsInstanceOf<ConflictObjectResult>(result);
        }

        [Test]
        public async Task Login_ReturnsOk_WithJwtToken_WhenCredentialsAreValid()
        {
            var loginDto = new LoginDto { Username = "testuser", Password = "password" };
            var user = new UserModel { Username = "testuser", Password = BCrypt.Net.BCrypt.HashPassword("password") };

            _mockUserRepository
                .Setup(repo => repo.GetUserByUsernameAsync(loginDto.Username))
                .ReturnsAsync(user);
            _mockJwtTokenService
                .Setup(service => service.GenerateToken(user))
                .Returns("valid_token");

            var result = await _controller.Login(loginDto);

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            ClassicAssert.NotNull(okResult);
            ClassicAssert.AreEqual("valid_token", (okResult.Value as dynamic).Token);
        }

        [Test]
        public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            var loginDto = new LoginDto { Username = "testuser", Password = "wrongpassword" };
            var user = new UserModel { Username = "testuser", Password = BCrypt.Net.BCrypt.HashPassword("password") };

            _mockUserRepository
                .Setup(repo => repo.GetUserByUsernameAsync(loginDto.Username))
                .ReturnsAsync(user);

            var result = await _controller.Login(loginDto);

            ClassicAssert.IsInstanceOf<UnauthorizedObjectResult>(result);
        }
    }
}
