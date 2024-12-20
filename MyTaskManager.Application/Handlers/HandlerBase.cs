using MyTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Handlers
{
    public abstract class HandlerBase
    {
        protected readonly IUserContextService UserContext;
        protected readonly ITodoService TodoService;

        protected HandlerBase(IUserContextService userContext, ITodoService todoService)
        {
            UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            TodoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
        }

        protected Guid GetCurrentUserId()
        {
            return UserContext.GetCurrentUserId();
        }
    }
}
