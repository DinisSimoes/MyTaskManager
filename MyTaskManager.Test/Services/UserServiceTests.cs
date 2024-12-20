using Moq;
using MyTaskManager.Application.Services;
using MyTaskManager.Domain.Entities;
using MyTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Test.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task IsUsernameTakenAsync_UsernameIsNullOrWhiteSpace_ThrowsArgumentException(string username)
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.IsUsernameTakenAsync(username));
            Assert.Equal("Username cannot be null or empty. (Parameter 'username')", exception.Message);
        }

        [Fact]
        public async Task IsUsernameTakenAsync_UsernameExists_ReturnsTrue()
        {
            // Arrange
            var username = "existinguser";
            _mockUserRepository.Setup(repo => repo.GetByUsernameAsync(username))
                .ReturnsAsync(new User { Username = username });

            // Act
            var result = await _userService.IsUsernameTakenAsync(username);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsUsernameTakenAsync_UsernameDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var username = "newuser";
            _mockUserRepository.Setup(repo => repo.GetByUsernameAsync(username))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.IsUsernameTakenAsync(username);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(null, "password")]
        [InlineData("", "password")]
        [InlineData(" ", "password")]
        public async Task CreateUserAsync_UsernameIsNullOrEmpty_ThrowsArgumentException(string username, string password)
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateUserAsync(username, password));
            Assert.StartsWith("Username cannot be null or empty.", exception.Message);
        }

        [Theory]
        [InlineData("username", null)]
        [InlineData("username", "")]
        [InlineData("username", "   ")]
        public async Task CreateUserAsync_PasswordIsNullOrEmpty_ThrowsArgumentException(string username, string password)
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateUserAsync(username, password));
            Assert.StartsWith("Password cannot be null or empty.", exception.Message);
        }

        [Fact]
        public async Task CreateUserAsync_ValidUser_CallsAddAsync()
        {
            // Arrange
            var username = "newuser";
            var password = "password";
            var user = new User { Username = username };

            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            await _userService.CreateUserAsync(username, password);

            // Assert
            _mockUserRepository.Verify(repo => repo.AddAsync(It.Is<User>(u => u.Username == username && u.PasswordHash != null)), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetUserByUsernameAsync_UsernameIsNullOrWhiteSpace_ThrowsArgumentException(string username)
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.GetUserByUsernameAsync(username));
            Assert.Equal("Username cannot be null or empty. (Parameter 'username')", exception.Message);
        }

        [Fact]
        public async Task GetUserByUsernameAsync_UserExists_ReturnsUser()
        {
            // Arrange
            var username = "existinguser";
            var user = new User { Username = username };
            _mockUserRepository.Setup(repo => repo.GetByUsernameAsync(username))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByUsernameAsync(username);

            // Assert
            Assert.Equal(username, result.Username);
        }

        [Fact]
        public async Task GetUserByUsernameAsync_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var username = "nonexistinguser";
            _mockUserRepository.Setup(repo => repo.GetByUsernameAsync(username))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetUserByUsernameAsync(username);

            // Assert
            Assert.Null(result);
        }
    }
}
