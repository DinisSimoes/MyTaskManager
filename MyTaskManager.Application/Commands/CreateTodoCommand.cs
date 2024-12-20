using MediatR;
using MyTaskManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Commands
{
    public class CreateTodoCommand : IRequest<TodoDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
