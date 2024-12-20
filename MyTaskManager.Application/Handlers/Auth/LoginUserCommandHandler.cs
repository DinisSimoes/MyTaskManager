using MediatR;
using MyTaskManager.Application.Commands;
using MyTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Handlers.Auth
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByUsernameAsync(request.Username);

            // Verifica se o usuário existe e valida a senha
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            // Gera o token JWT
            return _tokenService.GenerateToken(user);
        }
    }
}
