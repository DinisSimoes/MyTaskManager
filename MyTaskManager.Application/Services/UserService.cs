using MyTaskManager.Domain.Entities;
using MyTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));

            var user = await _userRepository.GetByUsernameAsync(username);
            return user != null;
        }

        public async Task CreateUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            // Criptografar a senha
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Criar o usuário
            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow
            };

            // Adicionar ao repositório
            await _userRepository.AddAsync(user);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));

            return await _userRepository.GetByUsernameAsync(username);
        }
    }
}
