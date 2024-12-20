using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Commands
{
    public class DeleteTodoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
