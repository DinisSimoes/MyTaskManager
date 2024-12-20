using MyTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Domain.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo?> GetByIdAsync(Guid id);
        Task<IEnumerable<Todo>> GetAllByUserIdAsync(Guid userId);
        Task AddAsync(Todo todo);
        Task UpdateAsync(Todo todo);
        Task DeleteAsync(Guid id);
    }
}
