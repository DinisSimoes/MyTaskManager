using MediatR;
using MyTaskManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Queries
{
    public class GetTodoByIdQuery : IRequest<TodoDto?>
    {
        public Guid Id { get; set; }
    }
}
