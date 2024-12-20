using MyTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Domain.Interfaces
{
    public interface ITodoService
    {
        Task<Todo?> GetByIdAsync(Guid id, Guid userId);
        Task<IEnumerable<Todo>> GetAllByUserIdAsync(Guid userId);
        Task AddAsync(Todo todo);
        Task UpdateAsync(Todo todo, Guid userId);
        Task DeleteAsync(Guid id, Guid userId);
    }
}
