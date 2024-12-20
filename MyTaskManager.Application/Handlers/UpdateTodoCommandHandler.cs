using MediatR;
using MyTaskManager.Application.Commands;
using MyTaskManager.Domain.Entities;
using MyTaskManager.Domain.Interfaces;

namespace MyTaskManager.Application.Handlers
{
    public class UpdateTodoCommandHandler : HandlerBase, IRequestHandler<UpdateTodoCommand, Unit>
    {

        public UpdateTodoCommandHandler(IUserContextService userContext, ITodoService todoService)
            : base(userContext, todoService)
        {
        }

        public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            Guid userId = GetCurrentUserId();

            var todo = new Todo
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description
            };

            try
            {
                await TodoService.UpdateAsync(todo, userId);
                return Unit.Value;
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("You cannot update this task.");
            }
        }
    }
}
