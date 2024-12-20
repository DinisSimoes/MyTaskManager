using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Commands
{
    public class RegisterUserCommand : IRequest<Unit>
    {
        public string Username { get; }
        public string Password { get; }

        public RegisterUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
