using MediatR;
using MyTaskManager.Application.DTOs;
using MyTaskManager.Application.Queries;
using MyTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Handlers
{
    public class GetTodoByIdQueryHandler : HandlerBase, IRequestHandler<GetTodoByIdQuery, TodoDto>
    {

        public GetTodoByIdQueryHandler(IUserContextService userContext, ITodoService todoService)
            : base(userContext, todoService)
        {
        }

        public async Task<TodoDto> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            Guid userId = GetCurrentUserId();

            var todo = await TodoService.GetByIdAsync(request.Id, userId);
            if (todo == null)
                return null; // Or throw an exception

            return new TodoDto
            {
                Title = todo.Title,
                Description = todo.Description
            };
        }
    }
}
