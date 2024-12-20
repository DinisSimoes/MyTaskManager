using MediatR;
using MyTaskManager.Application.DTOs;
using MyTaskManager.Application.Queries;
using MyTaskManager.Domain.Entities;
using MyTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Handlers
{
    public class GetAllTodosQueryHandler : HandlerBase, IRequestHandler<GetAllTodosQuery, IEnumerable<Todo>>
    {
        public GetAllTodosQueryHandler(IUserContextService userContext, ITodoService todoService)
            : base(userContext, todoService)
        {
        }

        public async Task<IEnumerable<Todo>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            Guid userId = GetCurrentUserId();

            var todos = await TodoService.GetAllByUserIdAsync(userId);

            return todos;
        }
    }
}
