using MediatR;
using MyTaskManager.Application.Commands;
using MyTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Handlers
{
    public class DeleteTodoCommandHandler : HandlerBase, IRequestHandler<DeleteTodoCommand, Unit>
    {

        public DeleteTodoCommandHandler(IUserContextService userContext, ITodoService todoService)
            : base(userContext, todoService)
        {
        }

        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            Guid userId = GetCurrentUserId();

            await TodoService.DeleteAsync(request.Id, userId);
            return Unit.Value;
        }
    }
}
