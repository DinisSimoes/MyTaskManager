using MediatR;
using MyTaskManager.Application.Commands;
using MyTaskManager.Application.DTOs;
using MyTaskManager.Domain.Entities;
using MyTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Handlers
{
    public class CreateTodoCommandHandler : HandlerBase, IRequestHandler<CreateTodoCommand, TodoDto>
    {
        public CreateTodoCommandHandler(IUserContextService userContext, ITodoService todoService)
            : base(userContext, todoService)
        {
        }

        public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            Guid userId = GetCurrentUserId();

            var todo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                CompletedAt = null,
                UserId = userId
            };

            await TodoService.AddAsync(todo);

            return new TodoDto
            {
                Title = todo.Title,
                Description = todo.Description
            };
        }
    }
}
