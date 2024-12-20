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
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userService.IsUsernameTakenAsync(request.Username))
                throw new InvalidOperationException("Username is already taken.");

            await _userService.CreateUserAsync(request.Username, request.Password);

            return Unit.Value;
        }
    }
}
