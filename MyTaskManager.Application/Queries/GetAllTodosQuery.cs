using MediatR;
using MyTaskManager.Application.DTOs;
using MyTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Queries
{
    public class GetAllTodosQuery : IRequest<IEnumerable<Todo>>
    {
    }
}
